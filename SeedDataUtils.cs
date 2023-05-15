using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
#pragma warning disable CS8604 // Possível argumento de referência nula.
namespace FSouzaSeedData
{
    /// <summary>
    /// This class is used to automatically insert records into the database using the Migrations Seed,
    /// obtaining the table name, fields names and values to be inserted conained in a list.
    /// </summary>
    public class SeedDataUtils
    {
        /// <summary>
        /// This method return the table name. It is necessary for the table to be annoted with he Table attribute
        /// Ex: [Table("my_table")]
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>table name</returns>
        public static string TableName(object entity)
        {
            return ((TableAttribute)entity.GetType().GetCustomAttribute(typeof(TableAttribute))).Name;
        }

        /// <summary>
        /// This method return the data from the list to be saved in database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static object[] Values(object entity)
        {
            List<object> propsDic = new();

            foreach (var prop in entity.GetType().GetProperties())
            {
                string column;
                string attribute = prop.Name;
                //TODO: identificar melhor estratégia para eliminar colunas não anotadas com [Column]
                try
                {
                    column = Column(attribute, entity);
                }
                catch (Exception)
                {
                    continue;
                }
                if (!attribute.EndsWith("Id"))
                    propsDic.Add(prop.GetValue(entity, null));
            }
            return propsDic.ToArray();
        }

        /// <summary>
        /// This method returns the list with the names of the columns annoted in the attribute with the annotations Column
        /// Ex: [Column("name")]
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string[] Columns(object entity)
        {
            List<string> propsDic = new();

            foreach (var prop in entity.GetType().GetProperties())
            {
                string column;
                string attribute = prop.Name;
                //TODO: identificar melhor estratégia para eliminar colunas não anotadas com [Column]
                try
                {
                    column = Column(attribute, entity);
                }
                catch (Exception)
                {
                    continue;
                }
                if (!attribute.EndsWith("Id"))
                    propsDic.Add(column);
            }
            return propsDic.ToArray();
        }

        /// <summary>
        /// This method return the name of the column annoted in the attribute with the annotation Column
        /// </summary>
        /// <param name="property"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string? Column(string property, object entity)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entity.GetType());
            ColumnAttribute columnAttribute = (ColumnAttribute)properties[property].Attributes[typeof(ColumnAttribute)];
            return columnAttribute.Name;
        }
    }
}
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
#pragma warning restore CS8604 // Possível argumento de referência nula.