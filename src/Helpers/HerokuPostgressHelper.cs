namespace HomeHealth.Helpers
{
    class HerokuPostgresHelper
    {
        // public string herokuurl = "postgres://erkuefejuhnhbn:0619346828110195676f39ab97207d1f3e09a32ba84c1fa94139dfc0d3ce2261@ec2-35-174-88-65.compute-1.amazonaws.com:5432/d6r0mg6r2qebhs";
        public string herokuurl;

        public HerokuPostgresHelper(string url){
            
            herokuurl = url;
        }

        public string buildConnectionString(){

            return $"Server={parseHost()};Port={parsePort()};User Id={parsetUser()};Password={parsePass()};Database={parseDatabase()}";
        }

        public string parseHost(){

            int pFrom = herokuurl.IndexOf("@") + "@".Length;
            int pTo = herokuurl.IndexOf(":",pFrom);

            return herokuurl.Substring(pFrom, pTo - pFrom);

        }

        public string parsetUser(){

            int pFrom = herokuurl.IndexOf("postgres://") + "postgres://".Length;
            int pTo = herokuurl.IndexOf(":",pFrom);

            return herokuurl.Substring(pFrom, pTo - pFrom);

        }

        public string parsePass(){

            int pFrom = herokuurl.IndexOf(parsetUser()) + parsetUser().Length + 1;
            int pTo = herokuurl.IndexOf("@",pFrom);

            return herokuurl.Substring(pFrom, pTo - pFrom);

        }

        public string parsePort(){

            int pFrom = herokuurl.IndexOf(parseHost()) + parseHost().Length + 1;
            int pTo = herokuurl.IndexOf("/",pFrom);

            return herokuurl.Substring(pFrom, pTo - pFrom);

        }

        public string parseDatabase(){

            int pFrom = herokuurl.IndexOf(parsePort()) + parsePort().Length + 1;
            int pTo = herokuurl.IndexOf("/",pFrom);

            return herokuurl.Substring(pFrom);
            
        }
        
    }
    
}