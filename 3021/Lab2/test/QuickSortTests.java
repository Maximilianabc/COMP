import org.junit.jupiter.api.DynamicTest;
import org.junit.jupiter.api.TestFactory;

import java.util.*;
import java.util.stream.Collectors;

import static org.junit.jupiter.api.Assertions.assertArrayEquals;

public class QuickSortTests {
    private final Map<int[], int[]> testInputs;

    {
        testInputs = new HashMap<>();
        testInputs.put(new int[]{10, 7, 8, 9, 1, 5}, new int[]{1, 5, 7, 8, 9, 10});
        testInputs.put(new int[]{9, 7}, new int[]{7, 9});
        testInputs.put(new int[]{5, 5}, new int[]{5, 5});
        testInputs.put(new int[]{1, 5}, new int[]{1, 5});
        testInputs.put(new int[]{49, 20, 5, 3, 1, 20, 5, 4, 8, 5}, new int[]{1, 3, 4, 5, 5, 5, 8, 20, 20, 49});
        testInputs.put(new int[]{9, 9, 9, 9, 9, 9}, new int[]{9, 9, 9, 9, 9, 9});
        testInputs.put(new int[]{9, 9, 1, 9, 11, 9}, new int[]{1, 9, 9, 9, 9, 11});
    }

    @TestFactory
    public Collection<DynamicTest> testFactory() {
        return testInputs.entrySet()
                .parallelStream()
                .map(entry -> DynamicTest.dynamicTest(
                        "test " + Arrays.stream(entry.getKey())
                                .mapToObj(String::valueOf)
                                .collect(Collectors.joining(",", "[", "]")),
                        () -> {
                            var arr = entry.getKey();
                            Main.quickSort(arr, 0, arr.length - 1);
                            assertArrayEquals(entry.getValue(), arr);
                        }
                ))
                .collect(Collectors.toList());

    }
}
