import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class TestLab5 {

    private static Person p;
    private static Student s;
    private static Teacher t;

    @BeforeAll
    static void setUp() {
        p = new Person("bob", 30);
        s = new Student("john", 19, 999, "CS");
        t = new Teacher("dylan", 50, "CS", 50000);
    }

    @DisplayName("Testing inheritance")
    @Test
    void testInheritance() {
        assertTrue(s instanceof Person);
        assertTrue(t instanceof Person);
        assertFalse(p instanceof Teacher);
        assertFalse(p instanceof Student);
    }

    @DisplayName("Testing the students's methods")
    @Test
    void testStudent() {
        assertEquals("john", s.getName());
        assertEquals(999, s.getId());
        assertEquals("CS", s.getMajor());
        assertTrue(s.toString().contains(s.getClass().getSimpleName()));
    }

    @DisplayName("Testing the teacher's methods")
    @Test
    void testTeacher() {
        assertEquals("dylan", t.getName());
        assertEquals(50, t.getAge());
        assertEquals("CS", t.getSubject());
        assertEquals(50000, t.getSalary());
        assertTrue(t.toString().contains(t.getClass().getSimpleName()));
    }

    @Disabled
    @Test
    void skipMe(){
        fail("purposely failing. skip this test with the @Disabled annotation!");
    }


}