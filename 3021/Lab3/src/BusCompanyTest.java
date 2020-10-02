import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BusCompanyTest {
    private static BusCompany c1;
    private static BusCompany c2;
    private static String COMPANY1 = "KMB";
    private static String COMPANY2 = "NWB";

    @BeforeAll
    static void initializeCompanies() {
        c1 = new BusCompany(COMPANY1);
        c2 = new BusCompany(COMPANY2);
    }

    @BeforeEach
    void resetBuses() {
        c1.removeAllBusses();
        c2.removeAllBusses();
    }

    @Test
    void testGetNames() {
        assertEquals(COMPANY1, c1.getName());
        assertEquals(COMPANY2, c2.getName());
    }

    @Test
    void testGetNumCompanies() {
        assertEquals(2, BusCompany.getNumCompanies());
    }

    @Test
    void testFailureAddDuplicate() {
        assertTrue(c1.createAndAddBus(1, "BMW"));
        assertFalse(c1.createAndAddBus(1, "Volvo"));
    }

    @Test
    void testSuccessAddDuplicateIdDifferentCompanies() {
        assertTrue(c1.createAndAddBus(1, "BMW"));
        assertTrue(c2.createAndAddBus(1, "BMW"));
    }

    @Test
    void testSuccessAdding5Buses() {
        assertTrue(c1.createAndAddBus(1, "BMW"));
        assertTrue(c1.createAndAddBus(2, "BMW"));
        assertTrue(c1.createAndAddBus(3, "BMW"));
        assertTrue(c1.createAndAddBus(4, "BMW"));
        assertTrue(c1.createAndAddBus(5, "BMW"));
    }

    @Test
    void testFailureAddingBusFull() {
        assertTrue(c1.createAndAddBus(1, "BMW"));
        assertTrue(c1.createAndAddBus(2, "BMW"));
        assertTrue(c1.createAndAddBus(3, "BMW"));
        assertTrue(c1.createAndAddBus(4, "BMW"));
        assertTrue(c1.createAndAddBus(5, "BMW"));
        assertFalse(c1.createAndAddBus(6, "BMW"));
    }
}