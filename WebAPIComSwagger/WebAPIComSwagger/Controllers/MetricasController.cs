using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Web.Http;
namespace WebAPIComSwagger.Controllers
{
    public class MetricasController : ApiController
    {
        [HttpPost]
        [Route("Metricas")]
        public JObject metricasService([FromBody] JObject metricasJson)
        {
            JObject retJson = new JObject();
            string param = metricasJson["param"].ToString();

            var items = new  List<Pessoa>();

            items.Add(new Pessoa { Nome = "Maycon Novaes", Idade = 28 });
            items.Add(new Pessoa { Nome = "Pessoa 1", Idade = 11 });

            var resultado = new ExcelService().ExportToExcel(ToDataTable(items));


            retJson.Add(new JProperty("file ", resultado));
 
            return retJson;
        }
        #region private

        private DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // cria colunas no datatable com o mesmo nome das propriedades públicas do tipo genérico
            foreach (PropertyInfo prop in properties)
            {
                var displayAttr = prop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                if (displayAttr != null)
                {
                    dataTable.Columns.Add(displayAttr.Name);
                }
                else
                {
                    dataTable.Columns.Add(prop.Name);
                }
            }

            // obtém todos os valores das propriedades do item e cria um DataRow que o represente
            foreach (T item in items)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            for (int col = dataTable.Columns.Count - 1; col >= 0; col--)
            {
                bool removeColumn = true;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (!row.IsNull(col))
                    {
                        removeColumn = false;
                        break;
                    }
                }
                if (removeColumn)
                    dataTable.Columns.RemoveAt(col);
            }

            return dataTable;
        }

        #endregion
    }



}
