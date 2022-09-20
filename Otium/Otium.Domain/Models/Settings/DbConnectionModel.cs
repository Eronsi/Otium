using System.Data.SqlClient;

namespace Otium.Domain.Models.Settings;

public class DbConnectionModel
{
    public string? DataSource { get; set; }
    public string? InitialCatalog { get; set; }
    public string? UserId { get; set; }
    public string? Password { get; set; }

    public string ConnectionString =>
        "data source=IBRAGIMOVS16\\SQLEXPRESS;initial catalog=Otium;trusted_connection=true";
    // new SqlConnectionStringBuilder
    // {
    //     DataSource = DataSource,
    //     InitialCatalog = InitialCatalog,
    //     IntegratedSecurity = true,
    //     UserID = UserId,
    //     Password = Password
    // }.ConnectionString;
}