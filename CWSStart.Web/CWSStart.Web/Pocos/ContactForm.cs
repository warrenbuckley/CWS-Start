using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace CWSStart.Web.Pocos
{
    [TableName("ContactFormLogs")]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class ContactForm
    {
        [Column("id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Message")]
        public string Message { get; set; }
    }
}