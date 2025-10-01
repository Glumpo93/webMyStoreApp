namespace webMyStoreApp.Components.Services
{
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    public class SchemeService
    {
        private readonly string _connString;

        public SchemeService(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public void CreateSchemesAndTables(string schemaName)
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                connection.Open();

                string createSchema = $"CREATE SCHEMA [{schemaName}]";
                string createCustomersTable = $@"
                CREATE TABLE [{schemaName}].[Customers] (
                    Code INT PRIMARY KEY,
                    Name NVARCHAR(100),
                    IdType TINYINT,
                    IdNumber INT,
                    PhoneNumber NVARCHAR(20),
                    AddressProvince TINYINT,
                    AddressCity TINYINT,
                    AddressDistrict TINYINT,
                    AddressDirections NVARCHAR(255),
                    EmailAddress NVARCHAR(255),
                    DeliveryAddress NVARCHAR(255)
                );";

                string createQuoteBillInfoTable = $@"
                CREATE TABLE [{schemaName}].[QuoteBillInfo] (
                    Number INT PRIMARY KEY,
                    DateTime DATETIME,
                    CurrencyCode NVARCHAR(10),
                    CustomerCode INT,
                    SubtotalTaxed DECIMAL(18, 2),
                    SubtotalExemption DECIMAL(18, 2),
                    SubtotalDiscount DECIMAL(18, 2),
                    TransportType INT,
                    TransportCharge DECIMAL(18, 2),
                    Taxes DECIMAL(18, 2),
                    Total DECIMAL(18, 2),
                    Notes NVARCHAR(500),
                    FOREIGN KEY (CustomerCode) REFERENCES [{schemaName}].[Customers](Code)
                );";

                string createQuoteBillLineTable = $@"
                CREATE TABLE [{schemaName}].[QuoteBillLine] (
                    Id INT PRIMARY KEY,
                    ItemCode NVARCHAR(50),
                    ItemName NVARCHAR(100),
                    ItemQuantity DECIMAL(18, 2),
                    UnitPrice DECIMAL(18, 2),
                    ItemDiscount DECIMAL(18, 2),
                    ItemNotes NVARCHAR(255),
                    QuoteBillNumber INT,
                    FOREIGN KEY (QuoteBillNumber) REFERENCES [{schemaName}].[QuoteBillInfo](Number)
                );";

                using (SqlCommand command = new SqlCommand(createSchema, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(createCustomersTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(createQuoteBillInfoTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(createQuoteBillLineTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
