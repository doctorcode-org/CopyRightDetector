using DoctorCode.DataProvider;
using System.Data.SqlClient;
using System.IO;

namespace CopyRightDetector.Core.Data
{
    public class DataProvider: FileStoreProvider
    {
        public int GetDocumentsCount()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM [tbl_FileStores]", cnn))
                {
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public int? DocumentExists(Stream stream)
        {
            var hash = ComputeHash(stream);
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("SELECT TOP(1) [DocumentId] FROM [tbl_FileStores] WHERE [HashSum] = @HashSum", cnn))
                {
                    cmd.Parameters.AddWithValue("@HashSum", hash);
                    return ((int?)cmd.ExecuteScalar());
                }
            }
        }

        

    }
}
