public class Student extends Person
{
    private int id;
    private String major;

    public Student(String Name, int Age)
    {
        super(Name, Age);
    }

    public Student(String Name, int Age, int ID, String Major)
    {
        super(Name, Age);
        id = ID;
        major = Major;
    }
    public String getMajor()
    {
        return major;
    }
    public int getId()
    {
        return id;
    }
}
