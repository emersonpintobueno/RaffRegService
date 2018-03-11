using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace RaffRegServer.Classes
{
    class SQL
    {
        public List<string> PegarInstSQL()
        {
            List<string> servers = new List<string>();

            // Get servers from the registry (if any)
            RegistryKey key = RegistryKey.OpenBaseKey(
              Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            key = key.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
            object installedInstances = null;
            if (key != null) { installedInstances = key.GetValue("InstalledInstances"); }
            List<string> instances = null;
            if (installedInstances != null) { instances = ((string[])installedInstances).ToList(); }
            if (System.Environment.Is64BitOperatingSystem)
            {
                /* The above registry check gets routed to the syswow portion of 
                 * the registry because we're running in a 32-bit app. Need 
                 * to get the 64-bit registry value(s) */
                key = RegistryKey.OpenBaseKey(
                        Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                key = key.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                installedInstances = null;
                if (key != null) { installedInstances = key.GetValue("InstalledInstances"); }
                string[] moreInstances = null;
                if (installedInstances != null)
                {
                    moreInstances = (string[])installedInstances;
                    if (instances == null)
                    {
                        instances = moreInstances.ToList();
                    }
                    else
                    {
                        instances.AddRange(moreInstances);
                    }
                }
            }
            foreach (string item in instances)
            {
                string name = System.Environment.MachineName;
                if (item != "MSSQLSERVER") { name += @"\" + item; }
                if (!servers.Contains(name.ToUpper())) { servers.Add(name.ToUpper()); }
            }

            return servers;
        }

        public List<string> PegarInstSQLRede()
        {
            List<string> servers = new List<string>();

            //DataTable dt = Microsoft.SqlServer.Management.Smo.SmoApplication.EnumAvailableSqlServers(false);

            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable dt = instance.GetDataSources();

            foreach (DataRow dtr in dt.Rows)
            {
                foreach (DataColumn dtc in dt.Columns)
                {
                    servers.Add(dtc.ColumnName + " - " + dtr[dtc]);
                }

            }
            return servers;
        }

        public List<string> SSql()
        {
            List<string> servSQL = new List<string>();
            ServiceController[] controllers = ServiceController.GetServices();
            foreach (var item in controllers)
            {
                if (item.ServiceName.Contains("MSSQL"))
                {
                    //MessageBox.Show("Achei");
                    servSQL.Add(item.ServiceName.ToString());
                }

            }
            return servSQL;
        }

        public Boolean StatusSQL(string svc)
        {
            ServiceController sc = new ServiceController();
            Boolean st = false;
            try
            {
                sc = new ServiceController(svc);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Serviço do SQL não encontrado");
            }

            if (sc.Status == ServiceControllerStatus.Running)
            {
                st = true;
            }

            return st;
        }

        public List<string> PegarListaBases(string banco)
        {
            List<string> list = new List<string>();

            //Abrir a conexão com a base
            //string conString = "server=" + banco + ";uid=sa;pwd=@H3xt0r;";
            string conString = "server=" + banco + ";uid=Raffinato;pwd=RaffinatoSenha;";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                // Set up a command with the given query and associate
                // this with the current connection.
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return list;

        }
    }
}
