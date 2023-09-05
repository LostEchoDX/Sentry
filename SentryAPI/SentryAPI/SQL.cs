using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

public static class SQL
{
    private static string connectionString = "Data Source=DESKTOP-CVP2KET\\SQLEXPRESS;Initial Catalog=Sentry;Integrated Security=True";
    private static string connectionStringLite = "Data Source=/app/bin/Debug/net6.0/Sentry.db;Version=3;";

    private static int executeQuery(string query)
    {
        // Initialization.  
        int rowCount = 0;
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        SqlCommand sqlCommand = new SqlCommand();

        try
        {
            // Settings.  
            sqlCommand.CommandText = query;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandTimeout = 12 * 3600;

            // Open.  
            sqlConnection.Open();

            // Result.  
            rowCount = sqlCommand.ExecuteNonQuery();

            // Close.  
            sqlConnection.Close();
        }
        catch (Exception)
        {
            // Close.  
            sqlConnection.Close();
        }

        return rowCount;
    }

    private static int executeQueryLite(string query)
    {
        // Initialization.  
        int rowCount = 0;
        SQLiteConnection sqlConnection = new SQLiteConnection(connectionStringLite);
        SQLiteCommand sqlCommand = new SQLiteCommand();

        try
        {
            // Settings.  
            sqlCommand.CommandText = query;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandTimeout = 12 * 3600;

            // Open.  
            sqlConnection.Open();

            // Result.  
            rowCount = sqlCommand.ExecuteNonQuery();

            // Close.  
            sqlConnection.Close();
        }
        catch (Exception)
        {
            // Close.  
            sqlConnection.Close();
        }

        return rowCount;
    }

    private static int executeQuery(string query, List<IDataParameter> sqlParameters)
    {
        int rowCount = 0;
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

        try
        {
            sqlConnection.Open();
            if (sqlParameters != null)
            {
                foreach (IDataParameter parameter in sqlParameters)
                    sqlCommand.Parameters.Add(parameter);

                rowCount = sqlCommand.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
        catch (Exception)
        {
            sqlConnection.Close();
        }
        return rowCount;
    }

    private static int executeQueryLite(string query, List<IDataParameter> sqlParameters)
    {
        int rowCount = 0;
        SQLiteConnection sqlConnection = new SQLiteConnection(connectionStringLite);
        SQLiteCommand sqlCommand = new SQLiteCommand(query, sqlConnection);

        try
        {
            sqlConnection.Open();
            if (sqlParameters != null)
            {
                foreach (IDataParameter parameter in sqlParameters)
                    sqlCommand.Parameters.Add(parameter);

                rowCount = sqlCommand.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            sqlConnection.Close();
        }
        return rowCount;
    }

    private static DataTable readQuery(string query)
    {
        DataTable result = new DataTable();
        SqlDataReader reader;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            using (reader = command.ExecuteReader())
                result.Load(reader);
        }
        return result;
    }

    private static DataTable readQueryLite(string query)
    {
        DataTable result = new DataTable();
        SQLiteDataReader reader;
        using (SQLiteConnection connection = new SQLiteConnection(connectionStringLite, true))
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            using (reader = command.ExecuteReader())
                result.Load(reader);
        }
        return result;
    }

    public static bool CreateEntry(string table, List<string> keys, List<object> values)
    {
        try
        {
            string query = "INSERT INTO " + "[" + table + "] (";
            List<IDataParameter> parameters = new List<IDataParameter>();
            foreach (string key in keys)
                query += "[" + key + "], ";

            query = query.Remove(query.Length - 2, 2) + ") Values (";
            foreach (string key in keys)
                query += "@" + key + ", ";

            query = query.Remove(query.Length - 2, 2) + ")";
            for (int i = 0; i < keys.Count; i++)
                parameters.Add(new SqlParameter("@" + keys[i], values[i]));

            executeQuery(query, parameters);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool UpdateEntry(string table, int id, List<string> keys, List<object> values)
    {
        try
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            string query = "UPDATE [" + table + "] SET ";

            foreach (string key in keys)
                query += "[" + key + "] = @" + key + ", ";

            query = query.Remove(query.Length - 2, 2);
            query += " WHERE ID = ";
            query += id;

            for (int i = 0; i < keys.Count; i++)
                parameters.Add(new SqlParameter("@" + keys[i], values[i]));

            executeQuery(query, parameters);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool DeleteEntry(int id, string table)
    {
        try
        {
            string query = "DELETE FROM [" + table + "] WHERE ID = '" + id + "'";
            executeQuery(query);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string ReadTable(string table)
    {
        string query = "SELECT * FROM [" + table + "]";
        DataTable data = readQuery(query);
        string result = JsonConvert.SerializeObject(data);
        return result;
    }

    public static string ReadLine(string table, string id)
    {
        string query = "SELECT * FROM [" + table + "] WHERE ID = " + id;
        DataTable data = readQuery(query);
        string result = JsonConvert.SerializeObject(data);
        return result;
    }


    public static bool CreateEntryLite(string table, List<string> keys, List<object> values)
    {
        try
        {
            string query = "INSERT INTO " + "[" + table + "] (";
            List<IDataParameter> parameters = new List<IDataParameter>();
            foreach (string key in keys)
                query += "[" + key + "], ";

            query = query.Remove(query.Length - 2, 2) + ") Values (";
            foreach (string key in keys)
                query += "@" + key + ", ";

            query = query.Remove(query.Length - 2, 2) + ")";
            for (int i = 0; i < keys.Count; i++)
                parameters.Add(new SQLiteParameter("@" + keys[i], values[i]));

            executeQueryLite(query, parameters);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool UpdateEntryLite(string table, int id, List<string> keys, List<object> values)
    {
        try
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            string query = "UPDATE [" + table + "] SET ";

            foreach (string key in keys)
                query += "[" + key + "] = @" + key + ", ";

            query = query.Remove(query.Length - 2, 2);
            query += " WHERE ID = ";
            query += id;

            for (int i = 0; i < keys.Count; i++)
                parameters.Add(new SqlParameter("@" + keys[i], values[i]));

            executeQueryLite(query, parameters);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool DeleteEntryLite(int id, string table)
    {
        try
        {
            string query = "DELETE FROM [" + table + "] WHERE ID = '" + id + "'";
            executeQueryLite(query);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string ReadTableLite(string table)
    {
        string query = "SELECT * FROM [" + table + "]";
        DataTable data = readQueryLite(query);
        string result = JsonConvert.SerializeObject(data);
        return result;
    }

    public static string ReadLineLite(string table, string id)
    {
        string query = "SELECT * FROM [" + table + "] WHERE ID = " + id;
        DataTable data = readQueryLite(query);
        string result = JsonConvert.SerializeObject(data);
        return result;
    }
}

