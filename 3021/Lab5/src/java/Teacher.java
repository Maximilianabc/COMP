
public class Teacher extends Person {
    private String subject;
    private double salary;

    public Teacher(String Name, int Age, String Subject, double Salary)
    {
        super(Name, Age);
        subject = Subject;
        salary = Salary;
    }

    public String getSubject()
    {
        return subject;
    }
    public double getSalary()
    {
        return salary;
    }
}
