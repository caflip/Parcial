using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data;
public static class BadDb
{
    private static string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BadCalcOrders;User Id=sa;Password=1234;TrustServerCertificate=True";

    public static void SetConnectionString(string connStr)
    {
        ConnectionString = connStr;
    }
    public static string GetConnectionString()
    {
        return ConnectionString;
    }
    public static int ExecuteNonQueryUnsafe(string sql)
    {
        var conn = new SqlConnection(ConnectionString);
        var cmd = new SqlCommand(sql, conn);
        conn.Open();
        return cmd.ExecuteNonQuery();
    }

    public static IDataReader ExecuteReaderUnsafe(string sql)
    {
        var conn = new SqlConnection(ConnectionString);
        var cmd = new SqlCommand(sql, conn);
        conn.Open();
        return cmd.ExecuteReader();
    }
}