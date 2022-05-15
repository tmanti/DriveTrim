using System;
using Npgsql;

namespace DriveTrimBackend.Utilities
{
    public class DBUtils
    {
        private NpgsqlConnection conn;
        

        public async void initDB()
        {
            string connstring = "Host=localhost;Username=drivetrim;Password=dbpass;Database=drivetrim";
            conn = new NpgsqlConnection(connstring);
            await conn.OpenAsync();

            //NOTE USER TEXT = JOB TEXT
            
            await using var cmd = new NpgsqlCommand(@"CREATE TABLE USERS(
                        USER TEXT,
                        JOBSTATUS BIT,
                        PRIMARY KEY (USER)
                    );", conn);

            await cmd.ExecuteNonQueryAsync();
        }

        public async void AddHist(string job, string album, string histid, string histjson)
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO (@p1)-(@p2) VALUES (@p3), (P4);", conn)
            {
                Parameters =
                {
                    new("p1", job),
                    new("p2", album),
                    new("p3", histid),
                    new("p4", histjson)
                }
            };
            
            await cmd.ExecuteNonQueryAsync();
        }

        public async void StartJob(string job)
        {
            await using var cmd = new NpgsqlCommand("SELECT JOBSTATUS FROM USERS WHERE USER = (@p1)")
            {
                Parameters =
                {
                    new("p1", job)
                }
            };
            
            await using var reader = await cmd.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                reader.Read();
                int val = Int32.Parse(reader[0].ToString());
                if (val == 0)
                {
                    return;
                }
                else
                {
                    //DROP ALL TABLES RELATED
                }
            }
            
            await using var batch = new NpgsqlBatch(conn)
            {
                BatchCommands =
                {
                    new ("INSERT INTO USERS VALUES (@p1), 0"){
                        Parameters =
                        {
                            new("p1", job),
                        }
                    },
                    new(@"CREATE TABLE (@p1)(
                        ALBUMID TEXT,
                        PRIMARY KEY (ALBUMID)
                    );")
                    {
                        Parameters =
                        {
                            new("p1", job),
                        },
                    },
                }
            };
            await batch.ExecuteNonQueryAsync(); 
        }

        public async void NewAlbum(string job, string album, string histid1, string histjson1, string histid2, string histjson2)
        {
            await using var batch = new NpgsqlBatch(conn)
            {
                BatchCommands =
                {
                    new("INSERT INTO (@p1) VALUES (@p1)-(@p2)")
                    {
                        Parameters =
                        {
                            new("p1", job),
                            new("p2", album),
                        }
                    },
                    new(@"CREATE TABLE (@p1)-(@p2)(
                        HISTID TEXT,
                        HISTJSON TEXT,
                        PRIMARY KEY (HISTID)
                    );")
                    {
                        Parameters =
                        {
                            new("p1", job),
                            new("p2", album),
                        }
                    },
                    new("INSERT INTO (@p1)-(@p2) VALUES (@p3), (P4);")
                    {
                        Parameters =
                        {
                            new("p1", job),
                            new("p2", album),
                            new("p3", histid1),
                            new("p4", histjson1),
                        }
                    },
                    
                    new("INSERT INTO (@p1)-(@p2) VALUES (@p3), (P4);")
                    {
                        Parameters =
                        {
                            new("p1", job),
                            new("p2", album),
                            new("p3", histid2),
                            new("p4", histjson2),
                        }
                    },
                }
            };

            await batch.ExecuteNonQueryAsync(); 
        }
    }
}