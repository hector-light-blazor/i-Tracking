using System.Security.Claims;
using System.DirectoryServices;

 void TestADAccount(string username, string password)
{

    string path = "LDAP://hidalgocounty.local/DC=HidalgoCounty,DC=Local";
    try
    {
        using (DirectoryEntry entry = new DirectoryEntry(path, username, password))
        {
            Console.WriteLine("WE DID IT: FIRST STEP");
            using (DirectorySearcher searcher = new DirectorySearcher(entry))
            {
                //Buscamos por la propiedad SamAccountName
                searcher.Filter = "(samaccountname=" + username + ")";
                //Buscamos el usuario con la cuenta indicada
                var result = searcher.FindOne();
                if (result != null)
                {

                    Console.WriteLine("WE FOUND IT YAY!");

                }
                else
                    Console.WriteLine("ERROR NO GOOD");
            }
        }

    }
    catch (Exception ex)
    {
       Console.WriteLine($"WE CACTH ERROR: {ex.Message}");
    }
}

Console.WriteLine("Welcome to Console App");
Console.WriteLine();
Console.WriteLine("---------=--------------");
Console.WriteLine("Enter Username: ");



string username = Console.ReadLine();


Console.WriteLine();
Console.WriteLine("---------=--------------");
Console.WriteLine("Enter Password: ");
string password = Console.ReadLine();
Console.WriteLine();

Console.WriteLine();
Console.WriteLine("---------=--------------");
Console.WriteLine($"Username: {username} | password: {password}");
Console.WriteLine();


TestADAccount(username, password);

Console.WriteLine();



Console.WriteLine();

Console.WriteLine("Press any key to exit");
Console.ReadLine();


