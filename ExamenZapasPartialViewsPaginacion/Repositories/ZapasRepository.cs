using ExamenZapasPartialViewsPaginacion.Data;
using ExamenZapasPartialViewsPaginacion.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExamenZapasPartialViewsPaginacion.Repositories
{
    #region SP
    /*
    alter procedure SP_GET_FOTOS_ZAPAS
	(@ID_ZAPA int, @POS int, @REGISTROS int out)
	as
		select @REGISTROS = COUNT(IDIMAGEN) from IMAGENESZAPASPRACTICA
			where IDPRODUCTO = @ID_ZAPA

		select IDIMAGEN, IDPRODUCTO, IMAGEN from
			(
				select CAST(ROW_NUMBER() over (order by IDIMAGEN) as int) as POS, IDIMAGEN, IDPRODUCTO, IMAGEN from IMAGENESZAPASPRACTICA
					where IDPRODUCTO = @ID_ZAPA
			)query 
			where POS = @POS
	go

	exec SP_GET_FOTOS_ZAPAS 1, 2, 0

    */
    #endregion

    public class ZapasRepository
    {
        private ZapasContext context;
        public ZapasRepository(ZapasContext context) 
        { 
            this.context = context;
        }

        public async Task<List<Zapas>> GetZapasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task<Zapas> FindZapasIdAsync(int id)
        {
            return await this.context.Zapatillas
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ModelZapasPagination> GetModelImagenPagAsync(int id, int pos)
        {

            ModelZapasPagination model = new ModelZapasPagination();

            string sql = "SP_GET_FOTOS_ZAPAS @ID_ZAPA, @POS, @REGISTROS OUT";

            SqlParameter pamId = new SqlParameter("@ID_ZAPA", id);
            SqlParameter pamPos = new SqlParameter("@POS", pos);
            SqlParameter pamRegistros = new SqlParameter("@REGISTROS", 0);

            pamRegistros.Direction = System.Data.ParameterDirection.Output;

            var consulta = this.context.Imagenes.FromSqlRaw(sql, pamId, pamPos, pamRegistros);

            List<Imagen> imagenes = await consulta.ToListAsync();
            int registros = Convert.ToInt32(pamRegistros.Value);
            model.Id = id;
            model.ImagenZapa = imagenes.First();
            model.Registros = registros;

            return model;
        }
    }
}
