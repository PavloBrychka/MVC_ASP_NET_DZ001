public class Note
{
    public string Name { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public List<string> Tags { get; set; }

    public Note()
    {
        Name = "testname";
        Text = "testtext";
        Date = DateTime.Now;
        Tags = new List<string>();
        Tags.Add("test1");
        Tags.Add("test2");
        Tags.Add("test3");

    }
}

public class View
{
    Controler controler =  new Controler();
    public List<Note> Notes { get; set; } = new List<Note>();

    public void PrintData()
    {
        var temp = controler.Print(this.Notes);
        Console.WriteLine(temp);
    }
    public void ReadData()
    {
        using (StreamReader sr = new StreamReader("temp.txt"))
        {
            while (sr.EndOfStream)
            {
                string name = sr.ReadLine();
                string text = sr.ReadLine();
                DateTime date = DateTime.Parse(sr.ReadLine());
                List<string> tags = new List<string>();
                while (sr.ReadLine() != "*")
                {
                    tags.Add(sr.ReadLine());
                }
                Note n = new Note();
                Notes.Add(n);
            }
        }
    }
    public void SaveFile()
    {
        var temp = controler.Print(this.Notes);
        
        using (StreamWriter sw = new StreamWriter("temp.txt"))
        {
            sw.WriteLine(temp.ToString());

        }
    }


}
public class Controler
{
    public string Print(List<Note> notes)
    {
        Model model = new Model();
        return model.GetData(notes);
    }
}
public class Model
{
    public string GetData(List<Note> notes)
    {
        string str = "";
        foreach (var note in notes)
        {


             str += note.Name + "\n" + note.Text + "\n" + Convert.ToString(note.Date) + "\n";
            foreach (var i in note.Tags)
            {
                str += i + " ";
            }
            str += "*";
            
        }
        return str;
    }
}

public class Program
{
    public static void Main()
    {

        View view = new View();
        while (true)
        {
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Print");
            Console.WriteLine("2 - Save to File");
            Console.WriteLine("3 - AddData");
            Console.WriteLine("4 - ReadFile");
            Console.Write("Enter menu__ ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if(x == 0)
            {
                Console.WriteLine("Goodbaye");
                break;
            }
            else if(x == 1)
            {
                
                view.PrintData();
            }
            else if(x==2)
            {
                view.SaveFile();
            }
            else if(x == 3)
            {
                Note temp = new Note();
                Console.Write("Enter Name__ ");
                temp.Name = Console.ReadLine();
                Console.Write("Enter Text__ ");
                temp.Text = Console.ReadLine();
                Console.Write("Enter Date (12.12.1999)__ ");
                temp.Date = DateTime.Parse(Console.ReadLine());

                while(true)
                {
                    Console.WriteLine("0 - Exit");
                    Console.WriteLine("1 - Add tag");
                    Console.Write("Enter menu__ ");
                    x = Convert.ToInt32(Console.ReadLine());
                    if(x == 0)
                    {
                        break;
                    }
                    else if(x == 1)
                    {
                        Console.WriteLine("Enter Tag");
                        string st = Console.ReadLine();
                        if(st == null || st.Length == 0)
                        {
                            continue;
                        }
                        else
                        {
                            temp.Tags.Add(st);
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("ERROR!");
                    }

                }
                view.Notes.Add(temp);
                if(x == 0)
                {
                    continue;
                }
            }
            else if(x == 4)
            {
                view.ReadData();
            }
            else
            {
                Console.WriteLine("ERROR!");
                continue;
            }
        }
    }
}
