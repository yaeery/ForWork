using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ServerUD
{
    class BD
    {
        SqlConnection sqlConnection;
        public BD(string Pyt)
        {
            sqlConnection = new SqlConnection(Pyt);
        }
        public BD() { }
        public void Open_Conection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public void Close_Conection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        public SqlConnection Get_Connection()
        {
            return sqlConnection;
        }
        public string Get_Type_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("Select Type from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "' and Is_Active = 'Y'", sqlConnection);
            //SqlDataReader reader = sqlCommand.ExecuteReader();
            object Type = sqlCommand_Cl.ExecuteScalar();
            SqlCommand sqlCommand_Sotr;
            if (Type == null)
            {
                sqlCommand_Sotr = new SqlCommand("Select Type from dbo.Сотрудник where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'  and Is_Active = 'Y'", sqlConnection);
                Type = sqlCommand_Sotr.ExecuteScalar();
            }
            if ((Type == null))
            {
                return '0'.ToString();
            }
            return Type.ToString();
        }
        private List<string> Decoding_LP(string Msg)
        {
            string Prom = "";
            List<string> List_Return = new List<string>();
            for (int j = 3; j < Msg.Length; j++)
            {
                if (Msg[j] == '#')
                {
                    List_Return.Add(Prom);
                    Prom = "";
                    continue;
                }
                else
                {
                    Prom += Msg[j];
                }
            }
            List_Return.Add(Prom);
            return List_Return;
        }
        public string Get_Psevdonim_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Sotr;
            sqlCommand_Sotr = new SqlCommand("Select Psevdonim from dbo.Сотрудник where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
            object Type_1 = sqlCommand_Sotr.ExecuteScalar();
            if (Type_1 != null)
            {
                return Type_1.ToString();
            }
            else
            {
                SqlCommand sqlCommand_Cl = new SqlCommand("dbo.Get_Psevdonim_of", sqlConnection);
                sqlCommand_Cl.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter LoginParam = new SqlParameter
                {
                    ParameterName = "@Login",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 10,
                    Value = List_For_Work[0]
                };
                SqlParameter PasswordParam = new SqlParameter
                {
                    ParameterName = "@Password",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 10,
                    Value = List_For_Work[1]
                };
                SqlParameter Itog = new SqlParameter
                {
                    ParameterName = "@Itog",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 10
                };
                sqlCommand_Cl.Parameters.Add(LoginParam);
                sqlCommand_Cl.Parameters.Add(PasswordParam);
                sqlCommand_Cl.Parameters.Add(Itog);
                sqlCommand_Cl.ExecuteNonQuery();
                string Type = Itog.Value.ToString();
                return Type.ToString();
            }
        }
        public string Get_Phone_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("Select Телефон from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
            object Type = sqlCommand_Cl.ExecuteScalar();
            if (Type == null)
            {
                SqlCommand sqlCommand_Sotr;
                sqlCommand_Sotr = new SqlCommand("Select Phone from dbo.Сотрудник where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                Type = sqlCommand_Sotr.ExecuteScalar();
            }
            return Type.ToString();
        }

        public string Get_FIO_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("Select ФИО from dbo.Сотрудник where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
            object Type = sqlCommand_Cl.ExecuteScalar();
            return Type.ToString();
        }
        public string Get_All_Meropriatie_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);//Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'"
            SqlCommand sqlCommand_Cl = new SqlCommand("select НазваниеМероприятия,ДатаПроведения from Мероприятие order by ДатаПроведения ", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            string Prom = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
                Return_String += Type[1].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Get_All_Usluga_of(string Msg)
        {
           // List<string> List_For_Work = Decoding_LP(Msg);//Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'"
            SqlCommand sqlCommand_Cl = new SqlCommand("select * from Услуга", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            string Prom = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
                Return_String += Type[1].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Get_Tren_by_Client_in_Rasspis_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);//Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'"
            SqlCommand sqlCommand_Cl = new SqlCommand("select НазваниеУслуги from Группа, УчастникГруппы, Клиент, Расписание where Расписание.ID_Группы = Группа.ID_Группы and Клиент.ID_клиента = УчастникГруппы.ID_Клиента and Группа.ID_Группы = УчастникГруппы.ID_Группы and ФИО = '"+List_For_Work[0]+"' and Дата ='"+(DateTime.Now).ToString() +"' group by НазваниеУслуги", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            string Prom = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Get_All_People_of(string Msg)
        {
            //List<string> List_For_Work = Decoding_LP(Msg);//Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'"
            SqlCommand sqlCommand_Cl;
            SqlDataReader Type;
            string Return_String = "";
            if (Msg[2] == 'C')
            {
                 sqlCommand_Cl = new SqlCommand("select ФИО,Is_Active from Клиент ", sqlConnection);
                 Type = sqlCommand_Cl.ExecuteReader();
                 while (Type.Read())
                 {
                    Return_String += Type[0].ToString();
                    Return_String += Type[1].ToString();
                    Return_String += "#";
                 }
                Type.Close();
            }
            else if (Msg[2] == 'A')
            {
                sqlCommand_Cl = new SqlCommand("select ФИО, ID_Сотрудника from Сотрудник where Type=2 and Is_Active ='Y'", sqlConnection);
                Type = sqlCommand_Cl.ExecuteReader();
                while (Type.Read())
                {
                    Return_String += Type[0].ToString();
                    Return_String += "#";
                    Return_String += Type[1].ToString();
                    Return_String += "#";
                }
                Type.Close();
            }
            else if(Msg[2]=='T')
            {
                sqlCommand_Cl = new SqlCommand("select ФИО, Is_Active from Сотрудник where Type<>4 ", sqlConnection);
                Type = sqlCommand_Cl.ExecuteReader();
                while (Type.Read())
                {
                    Return_String += Type[0].ToString();
                    Return_String += Type[1].ToString();
                    Return_String += "#";
                }
                Type.Close();
            }
            return Return_String.Remove(Return_String.Length - 1);
        }
        public string Get_Spisok_Uchactnikov_Mer_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);//Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'"
            SqlCommand sqlCommand_Cl = new SqlCommand("select ФИО from Клиент, Мероприятие, УчастникМероприятия where Клиент.ID_клиента = УчастникМероприятия.ID_Клиента and Мероприятие.ID_Мероприятия = УчастникМероприятия.ID_Мероприятия and Мероприятие.НазваниеМероприятия='"+ List_For_Work[0]+ "' and Мероприятие.ДатаПроведения='"+ List_For_Work[1] + "'", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String!="")
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
            else
            {
                return "F";
            }
        }
        public string Get_Meropriatie_User_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);//
            SqlCommand sqlCommand_Cl = new SqlCommand("select НазваниеМероприятия, ДатаПроведения from УчастникМероприятия, Клиент, Мероприятие where УчастникМероприятия.ID_Мероприятия=Мероприятие.ID_Мероприятия and УчастникМероприятия.ID_Клиента = Клиент.ID_клиента and Клиент.Login=  '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "' order by ДатаПроведения", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            string Prom = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
                Return_String += Type[1].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Set_Name_Pochta_Phone_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);//добавить тригер
            string Type = "";
            if (List_For_Work[List_For_Work.Count - 1] == "CL")
            {
                Type = "Клиент";
            }
            else if(List_For_Work[List_For_Work.Count - 1] == "TE")
            {
                Type = "Сотрудник";
            }
                try
                {   
                        if ((List_For_Work.Count > 4) && (List_For_Work[4] == "SK"))
                        {
                            SqlCommand sqlCommand_Cl = new SqlCommand("update dbo."+ Type + " set Login ='" + List_For_Work[2 + 1] + "',Password =  '" + List_For_Work[4 + 1] + "'  where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                            object Itog = sqlCommand_Cl.ExecuteNonQuery();
                            return "G";
                        }
                        else if (List_For_Work[2] == "SL")
                        {
                            SqlCommand sqlCommand_Cl = new SqlCommand("update dbo." + Type + "  set Login ='" + List_For_Work[2 + 1] + "' where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                            object Itog = sqlCommand_Cl.ExecuteNonQuery();
                            return "G";
                        }
                        else if (List_For_Work[2] == "SK")
                        {
                            SqlCommand sqlCommand_Cl = new SqlCommand("update dbo." + Type + "  set Password ='" + List_For_Work[2 + 1] + "' where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                            object Itog = sqlCommand_Cl.ExecuteNonQuery();
                            return "G";
                        }
                }
                catch(Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    return "E";
                }
                try
                {
                    for (int i = 2; i < List_For_Work.Count - 1; i += 2) // логин - пароль - инст - парам - инст - парам - нист - парам
                    {
                        if (List_For_Work[i] == "SN")
                        {
                            SqlCommand sqlCommand_Cl = new SqlCommand("update "+ Type + " set Psevdonim ='" + List_For_Work[i + 1] + "' where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                            object Itog = sqlCommand_Cl.ExecuteNonQuery();
                        }
                        else if (List_For_Work[i] == "SF")
                        {
                            SqlCommand sqlCommand_Cl = new SqlCommand("update "+ Type + " set Телефон ='" + List_For_Work[i + 1] + "' where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                            object Itog = sqlCommand_Cl.ExecuteNonQuery();
                        }
                        else if (List_For_Work[i] == "SP")
                        {
                            SqlCommand sqlCommand_Cl = new SqlCommand("update "+ Type + " set email ='" + List_For_Work[i + 1] + "' where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                            object Itog = sqlCommand_Cl.ExecuteNonQuery();
                        }
                    }
                    return "S".ToString();
                }
                catch
                {
                    return "F".ToString();
                }
           // SqlCommand sqlCommand_Cl = new SqlCommand("update dbo.Клиент set Psevdonim ='" + List_For_Work[2] + "' where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
           // object Itog = sqlCommand_Cl.ExecuteNonQuery();
        }
        //public string Set_Phone_of(string Msg)
        //{
        //    List<string> List_For_Work = Decoding_LP(Msg);
        //    SqlCommand sqlCommand_Cl = new SqlCommand("update dbo.Клиент set Телефон ='" + List_For_Work[2] + "' where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
        //    object Itog = sqlCommand_Cl.ExecuteNonQuery();
        //    //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
        //    //object Type = sqlCommand_Cl.ExecuteScalar();
        //    return "~".ToString();
        //}
        //public string Set_Pochta_of(string Msg)
        //{
        //    List<string> List_For_Work = Decoding_LP(Msg);
        //    SqlCommand sqlCommand_Cl = new SqlCommand("update dbo.Клиент set email ='" + List_For_Work[2] + "' where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
        //    object Itog = sqlCommand_Cl.ExecuteNonQuery();
        //    //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
        //    //object Type = sqlCommand_Cl.ExecuteScalar();
        //    return "~".ToString();
        //}
        public string Set_New_Usluga(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("insert into Услуга (Название, Цена) values ('"+ List_For_Work[0]+ "','"+ List_For_Work[1]+ "')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_New_Pokupka(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("insert into Продажа values((select ID_клиента from Клиент where ФИО = '"+List_For_Work[0]+"'),'"+List_For_Work[1]+"','"+DateTime.Now.ToString()+"')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_New_Group(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("insert into Группа  values ((Select count(*)+1 from Группа),(select ID_Сотрудника from Сотрудник where ФИО ='"+ List_For_Work[0] + "'),'" + List_For_Work[1] + "')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_eropriatye_User_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                List_For_Work[3] = DateTime.Parse(List_For_Work[3]).ToString("yyyy-MM-dd");
                SqlCommand sqlCommand_Cl = new SqlCommand("insert into УчастникМероприятия (ID_Клиента, ID_Мероприятия) values ( (select Клиент.ID_клиента from Клиент where Login='"+ List_For_Work[0]+ "' and Password='"+ List_For_Work[1] + "'),(select Id_Мероприятия from Мероприятие where НазваниеМероприятия='"+ List_For_Work[2]+ "' and ДатаПроведения = '"+List_For_Work[3] + "') )", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_New_Group_User_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("delete from УчастникГруппы where ID_Клиента = (select ID_Клиента from Клиент where ФИО = '" + List_For_Work[0] + "')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                if(List_For_Work.Count>1)
                {
                    for (int i = 1; i < List_For_Work.Count; i++)
                    {
                        SqlCommand sqlCommand_Cl2 = new SqlCommand("insert into УчастникГруппы  (ID_Клиента,ID_Группы) values((select ID_клиента from Клиент where ФИО='" + List_For_Work[0] + "'),'"+ List_For_Work[i] + "')" , sqlConnection);
                        object Itog2 = sqlCommand_Cl2.ExecuteNonQuery();
                    }
                }
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_Zp_Sotr_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("update dbo.Сотрудник set ЗаработнаяПлата ='" + List_For_Work[0] + "' where ФИО= '" + List_For_Work[1] + "'", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_New_Teacher_of_Group_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("update Группа set ID_Сотрудника ='"+ List_For_Work[1] + "' where ID_Группы='"+ List_For_Work[0] + "' ", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string New_Coast_Usluga_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("update Услуга set Цена = '"+ List_For_Work[1] + "' where Название ='" + List_For_Work[0] + "'", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Delete_Sotr_Or_Client_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                if (Msg[2]=='S')
                {
                    SqlCommand sqlCommand_Cl = new SqlCommand("update dbo.Сотрудник set Is_Active = 'F' where ФИО ='"+ List_For_Work[0] + "'", sqlConnection);
                    object Itog = sqlCommand_Cl.ExecuteNonQuery();
                }
                else if (Msg[2] == 'K')
                {
                    SqlCommand sqlCommand_Cl = new SqlCommand("update dbo.Клиент set Is_Active = 'F' where ФИО ='" + List_For_Work[0] + "'", sqlConnection);
                    object Itog = sqlCommand_Cl.ExecuteNonQuery();
                }
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_New_Meropriatie_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                List_For_Work[3] = DateTime.Parse(List_For_Work[3]).ToString("yyyy-MM-dd");
                SqlCommand sqlCommand_Cl = new SqlCommand("insert into Мероприятие (ID_Мероприятия, НазваниеМероприятия, МестоПроведения,Тип,ДатаПроведения) values ((Select count(*)+1 from Мероприятие),'"+ List_For_Work[0] + "','"+ List_For_Work[1] + "','"+List_For_Work[2]+"','"+ List_For_Work[3] + "')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_New_Rasspis_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("insert into Расписание (Дата, Время, ID_Группы) values ('" + List_For_Work[1] + "','" + List_For_Work[2] + "','" + List_For_Work[0] + "')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_New_Client_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("insert into Клиент  values ((select count(*) +1 from Клиент),'" + List_For_Work[0] + "','" + List_For_Work[1] + "','" + List_For_Work[2] + "','" + List_For_Work[3] + "','" + List_For_Work[4] + "','" + List_For_Work[5] + "','1','" + List_For_Work[6] + "','Y')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Set_New_Sotr_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("insert into Сотрудник  values ((select count(*) +1 from Сотрудник),'" + List_For_Work[0] + "','" + List_For_Work[1] + "','" + List_For_Work[2] + "','"+List_For_Work[3]+"','"+ List_For_Work[4] + "','"+ List_For_Work[5] + "','"+ List_For_Work[6] + "','"+ List_For_Work[7] + "','"+ List_For_Work[8] + "','"+ List_For_Work[9] + "','"+ List_For_Work[10] + "','Y')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "F".ToString();
            }
        }
        public string Delete_eropriatye_User_of(string Msg)
        {
            try
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("delete from УчастникМероприятия where ID_Клиента = (select Клиент.ID_клиента from Клиент where Login='" + List_For_Work[0] + "' and Password='" + List_For_Work[1] + "') and ID_Мероприятия=(select Id_Мероприятия from Мероприятие where НазваниеМероприятия='" + List_For_Work[2] + "' and ДатаПроведения = '" + List_For_Work[3] + "')", sqlConnection);
                object Itog = sqlCommand_Cl.ExecuteNonQuery();
                //SqlCommand sqlCommand_Cl = new SqlCommand("Select Psevdonim from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                //object Type = sqlCommand_Cl.ExecuteScalar();
                return "S".ToString();
            }
            catch
            {
                return "F".ToString();
            }
        }
        public string Get_Pochta_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("Select email from dbo.Клиент where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
            object Type = sqlCommand_Cl.ExecuteScalar();
            if (Type == null)
            {
                SqlCommand sqlCommand_Sotr = new SqlCommand("Select Pochta from dbo.Сотрудник where Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                Type = sqlCommand_Sotr.ExecuteScalar();
            }
            return Type.ToString();
        }
        public string Get_Inf_About_Studen_For_Director(string Msg)//Копию
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("select Телефон, email, Is_Active from Клиент where  ФИО = '" + List_For_Work[0] + "' group by Телефон, email, Is_Active", sqlConnection);/*and Is_Active = 'Y'*/
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
                Return_String += Type[1].ToString();
                Return_String += "#";
                Return_String += Type[2].ToString();
                Return_String += "#";
            }
            Type.Close();
            sqlCommand_Cl = new SqlCommand("select Группа.ID_Группы, Группа.НазваниеУслуги  from Клиент, УчастникГруппы, Группа where  Клиент.ID_клиента = УчастникГруппы.ID_Клиента and УчастникГруппы.ID_Группы = Группа.ID_Группы and ФИО = '" + List_For_Work[0] + "'", sqlConnection);
            SqlDataReader Type2 = sqlCommand_Cl.ExecuteReader();
            while (Type2.Read())
            {
                Return_String += Type2[0].ToString();
                Return_String += "#";
                Return_String += Type2[1].ToString();
                Return_String += "#";
            }
            Type2.Close();

            return Return_String.Remove(Return_String.Length - 1);
        }

        public string Get_Inf_About_Studen_For_Teacher(string Msg)//Копию
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("select Телефон, email from Клиент, УчастникГруппы, Группа where  Клиент.ID_клиента = УчастникГруппы.ID_Клиента and УчастникГруппы.ID_Группы = Группа.ID_Группы and ФИО = '" + List_For_Work[0] + "' and Is_Active='Y' group by Телефон, email", sqlConnection);/*and Is_Active = 'Y'*/
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
                Return_String += Type[1].ToString();
                Return_String += "#";
                //Return_String += Type[2].ToString();
                //Return_String += "#";
            }
            Type.Close();
            sqlCommand_Cl = new SqlCommand("select Группа.ID_Группы, Группа.НазваниеУслуги  from Клиент, УчастникГруппы, Группа where  Клиент.ID_клиента = УчастникГруппы.ID_Клиента and УчастникГруппы.ID_Группы = Группа.ID_Группы and ФИО = '" + List_For_Work[0] + "' and Is_Active='Y'", sqlConnection);
            SqlDataReader Type2 = sqlCommand_Cl.ExecuteReader();
            while (Type2.Read())
            {
                Return_String += Type2[0].ToString();
                Return_String += "#";
                Return_String += Type2[1].ToString();
                Return_String += "#";
            }
            Type2.Close();

            return Return_String.Remove(Return_String.Length - 1);
        }
        public string Get_Spisok_Groups(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("select Клиент.ФИО from Клиент, УчастникГруппы where Клиент.ID_клиента = УчастникГруппы.ID_Клиента and ID_Группы= '" + List_For_Work[0] + "'  and Is_Active = 'Y'", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Get_Spisok_Clientov_Poisk(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("select ФИО from Клиент where ФИО Like '%"+ List_For_Work[0]+ "%'", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Get_Spisok_Sotr_Poisk(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("select ФИО from Сотрудник where ФИО Like '%" + List_For_Work[0] + "%'", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Get_Spisok_Mer_Poisk(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("select НазваниеМероприятия,ДатаПроведения from (select НазваниеМероприятия,ДатаПроведения from Мероприятие left join УчастникМероприятия on Мероприятие.ID_Мероприятия = УчастникМероприятия.ID_Мероприятия where НазваниеМероприятия Like '%"+ List_For_Work[4] + "%' and ДатаПроведения between '"+ List_For_Work[2] + "' and '"+ List_For_Work[3] + "'  group by НазваниеМероприятия,ДатаПроведения having (count(Мероприятие.ID_Мероприятия ) >='"+List_For_Work[0]+"' and count(Мероприятие.ID_Мероприятия ) <='"+ List_For_Work[1] + "') ) as T", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            while (Type.Read())
            {
                Return_String += Type[0].ToString();
                Return_String += "#";
                Return_String += Type[1].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Get_Passpis_of(string Msg)     
        {
            if (Msg[2] == 'B')
            {
                SqlCommand sqlCommand_Sotr;
                sqlCommand_Sotr = new SqlCommand("select * from Расписание where Дата >'9-12-2023'", sqlConnection);
                SqlDataReader Type = sqlCommand_Sotr.ExecuteReader();
                string Return_String = "";
                string Prom = "";
                while (Type.Read())
                {
                    Return_String += Type[2].ToString();
                    Return_String += "#";
                    Return_String += (((DateTime)Type[0]).ToString("d MMMM yyyy")).ToString();
                    Return_String += "#";
                    Return_String += Type[1].ToString();
                    Return_String += "#";
                }
                Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
            else
            {
                List<string> List_For_Work = Decoding_LP(Msg);
                SqlCommand sqlCommand_Cl = new SqlCommand("select Расписание.Дата, Расписание.Время, Группа.НазваниеУслуги from Группа, УчастникГруппы, Клиент, Расписание where Расписание.ID_Группы = УчастникГруппы.ID_Группы and Группа.ID_Группы =  УчастникГруппы.ID_Группы and Клиент.ID_клиента = УчастникГруппы.ID_Клиента and Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "' and  Дата >'27-11-2023'", sqlConnection);
                SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
                if (Type.HasRows == false)
                {
                    Type.Close();
                    SqlCommand sqlCommand_Sotr;
                    sqlCommand_Sotr = new SqlCommand("select Расписание.Дата, Расписание.Время, Группа.НазваниеУслуги from Группа, Сотрудник, Расписание where Расписание.ID_Группы = Группа.ID_Группы  and Сотрудник.ID_Сотрудника = Группа.ID_Сотрудника and Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "' and  Дата >'27-11-2023'", sqlConnection);
                    Type = sqlCommand_Sotr.ExecuteReader();
                }
                string Return_String = "";
                string Prom = "";
                while (Type.Read())
                {
                    Return_String += (((DateTime)Type[0]).ToString("d MMMM")).ToString();
                    Return_String += "#";
                    Prom = Type[1].ToString();
                    Return_String += Prom.Remove(Prom.Length - 8);
                    Return_String += "#";
                    Return_String += Type[2].ToString();
                    Return_String += "#";
                }
                Type.Close();

                if (Return_String == "")
                {
                    return "F";
                }
                else
                {
                    return Return_String.Remove(Return_String.Length - 1);
                }
            }
        }

        public string Get_Groops_For_Dir_of(string Msg)
        {
            //List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_St = new SqlCommand("select Id_Группы from Группа", sqlConnection);
            SqlDataReader Type1 = sqlCommand_St.ExecuteReader();
            string Return_String = "";
            while (Type1.Read())
            {
                Return_String += Type1[0].ToString();
                Return_String += "#";
            }
            Type1.Close();
            return Return_String.Remove(Return_String.Length - 1);
        }
        public string Get_All_Pokupki_of(string Msg)
        {
            //List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_St = new SqlCommand("select ФИО, НазваниеУслуги, ДатаПродажи from Клиент, Продажа where Клиент.ID_клиента = Продажа.ID_Клиента and ДатаПродажи>'"+DateTime.Now.AddDays(-30)+"'", sqlConnection);
            SqlDataReader Type1 = sqlCommand_St.ExecuteReader();
            string Return_String = "";
            while (Type1.Read())
            {
                Return_String += Type1[0].ToString();
                Return_String += "#";
                Return_String += Type1[1].ToString();
                Return_String += "#";
                Return_String += ((DateTime)Type1[2]).ToString("d MMMM yyyy");
                Return_String += "#";
            }
            Type1.Close();
            return Return_String.Remove(Return_String.Length - 1);
        }
        public string Get_Sotr_Info_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_St = new SqlCommand("select Должность,ДатаРождения,Стаж,Phone,Pochta,ЗаработнаяПлата,Is_Active from Сотрудник where ФИО ='" + List_For_Work[0] + "'", sqlConnection);
            SqlDataReader Type1 = sqlCommand_St.ExecuteReader();
            string Return_String = "";
            while (Type1.Read())
            {
                Return_String += Type1[0].ToString();
                Return_String += "#";
                Return_String += Type1[1].ToString();
                Return_String += "#";
                Return_String += Type1[2].ToString();
                Return_String += "#";
                Return_String += Type1[3].ToString();
                Return_String += "#";
                Return_String += Type1[4].ToString();
                Return_String += "#";
                Return_String += Type1[5].ToString();
                Return_String += "#";
                Return_String += Type1[6].ToString();
                Return_String += "#";
            }
            Type1.Close();
            sqlCommand_St = new SqlCommand("select Группа.ID_Группы, Группа.НазваниеУслуги  from  Сотрудник, Группа where Сотрудник.ID_Сотрудника = Группа.ID_Сотрудника    and ФИО = '" + List_For_Work[0] + "'", sqlConnection);
            SqlDataReader Type2 = sqlCommand_St.ExecuteReader();
            while (Type2.Read())
            {
                Return_String += Type2[0].ToString();
                Return_String += "#";
                Return_String += Type2[1].ToString();
                Return_String += "#";
            }
            Type2.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }
        public string Get_Groops_of(string Msg)//Спросить по поводу прогрузки чатов и даления сообения
        {
            List<string> List_For_Work = Decoding_LP(Msg);//Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'"
            SqlCommand sqlCommand_Cl = new SqlCommand("select НазваниеУслуги, Сотрудник.ФИО, Группа.ID_Группы from Сотрудник, Группа, УчастникГруппы, Клиент where Сотрудник.ID_Сотрудника = Группа.ID_Сотрудника and Группа.ID_Группы = УчастникГруппы.ID_Группы and УчастникГруппы.ID_Клиента = Клиент.ID_клиента and Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            if (Type.HasRows!=false)
            {
                string Return_String = "";
                string Prom = "";
                while (Type.Read())
                {
                    Return_String += Type[0].ToString();
                    Return_String += "#";
                    Prom = Type[1].ToString();
                    Return_String += Prom.Remove(Prom.Length - 5);
                    Return_String += "#";
                    Return_String += Type[2].ToString();
                    Return_String += "#";
                }
                Type.Close();
                if (Return_String == "")
                {
                    return "F";
                }
                else
                {
                    return Return_String.Remove(Return_String.Length - 1);
                }
            }
            else
            {
                Type.Close();
                SqlCommand sqlCommand_St = new SqlCommand("select Id_Группы, НазваниеУслуги from Группа, Сотрудник where Сотрудник.ID_Сотрудника = Группа.ID_Сотрудника and Login= '" + List_For_Work[0] + "' and Password = '" + List_For_Work[1] + "'", sqlConnection);
                SqlDataReader Type1 = sqlCommand_St.ExecuteReader();
                string Return_String = "";  
                while (Type1.Read())
                {
                    Return_String += Type1[0].ToString();
                    Return_String += "#";
                    Return_String += Type1[1].ToString();
                    Return_String += "#";
                }
                Type1.Close();
                if (Return_String == "")
                {
                    return "F";
                }
                else
                {
                    return Return_String.Remove(Return_String.Length - 1);
                }
            }
        }
        public string Get_All_Inf_Groops_of(string Msg)//Спросить по поводу прогрузки чатов и даления сообения
        {
            List<string> List_For_Work = Decoding_LP(Msg);//Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'"
            SqlCommand sqlCommand_Cl = new SqlCommand("select *from Группа", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
                string Return_String = "";
                string Prom = "";
                while (Type.Read())
                {
                    Return_String += Type[0].ToString();
                    Return_String += "#";
                    Return_String += Type[1].ToString();
                    Return_String += "#";
                    Return_String += Type[2].ToString();
                    Return_String += "#";
                }
                Type.Close();
                if (Return_String == "")
                {
                    return "F";
                }
                else
                {
                    return Return_String.Remove(Return_String.Length - 1);
                }
        }
            public string Get_Pokupka_of(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            SqlCommand sqlCommand_Cl = new SqlCommand("Select Продажа.ДатаПродажи, Продажа.НазваниеУслуги from Продажа, Клиент where Продажа.ID_Клиента = Клиент.ID_клиента and Клиент.Login= '" + List_For_Work[0] + "' and Клиент.Password = '" + List_For_Work[1] + "'", sqlConnection);
            SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
            string Return_String = "";
            while (Type.Read())
            {
                Return_String += (((DateTime)Type[0]).ToString("d MMMM")).ToString();
                Return_String += "#";
                Return_String += Type[1].ToString();
                Return_String += "#";
            }
            Type.Close();
            if (Return_String == "")
            {
                return "F";
            }
            else
            {
                return Return_String.Remove(Return_String.Length - 1);
            }
        }

        public string All_About_Massage(string Msg)
        {
            List<string> List_For_Work = Decoding_LP(Msg);
            string Prom_Vibor = "";
            if (Msg[2] == 'G')
            {
                SqlCommand sqlCommand_Cl = new SqlCommand("Select * from MsgHran where Type='" + List_For_Work[0] + "'", sqlConnection);
                SqlDataReader Type = sqlCommand_Cl.ExecuteReader();
                string Return_String = "";
                while (Type.Read())
                {
                    Return_String += Type[0].ToString();
                    Return_String += "#";
                    Return_String += Type[2].ToString();
                    Return_String += "#";
                    Return_String += (((DateTime)Type[3]).ToString("HH:mm dd MMMM yyyy ")).ToString();
                    Return_String += "#";
                }
                Type.Close();
                if (Return_String == "")
                {
                    return "F";
                }
                else
                {
                    Prom_Vibor = Return_String.Remove(Return_String.Length - 1);
                }
            }
            else if(Msg[2]=='S')
            {
                string Type_Sotr = "";
                if(List_For_Work[0]!="0")
                {
                    SqlCommand sqlCommand_Cl = new SqlCommand("select ФИО from Сотрудник, Группа where Сотрудник.ID_Сотрудника = Группа.ID_Сотрудника and ID_Группы='"+ List_For_Work[0] + "'", sqlConnection);
                    object Type = sqlCommand_Cl.ExecuteScalar();
                    Type_Sotr = Type.ToString();
                }
                else
                {
                    if(List_For_Work[1]=="D")
                    {
                        Type_Sotr = "Директор";
                    }
                    else if(List_For_Work[1]=="A")
                    {
                        Type_Sotr = "Администратор";
                    }
                }
                try
                {
                    SqlCommand sqlCommand_Cl = new SqlCommand("insert into MsgHran (Sender, Type, Message, Date) values ('"+ Type_Sotr + "','"+ List_For_Work[0] + "','"+ List_For_Work[2] + "','"+DateTime.Now.ToString()+"' )", sqlConnection);
                    object Itog = sqlCommand_Cl.ExecuteNonQuery();
                    Prom_Vibor = "S";
                }
                catch
                {
                    Prom_Vibor = "F";
                }
            }

            return Prom_Vibor;

        }
    }
}   
