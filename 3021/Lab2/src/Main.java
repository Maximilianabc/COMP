public class Main {

    /**
     * The main function that implements QuickSort
     *
     * @param arr  array to be sorted
     * @param low  starting index
     * @param high ending index
     */
    public static void quickSort(int[] arr, int low, int high) {
        if (low < high) {
        /* pi is partitioning index, arr[p] is now
        at right place */
            int pi = partition(arr, low, high);

            // Separately sort elements before
            // partition and after partition
            quickSort(arr, low, pi - 1);
            quickSort(arr, pi + 1, high);
        }
    }

    public static void printArray(int[] arr) {
        int i;
        for (i = 0; i < arr.length; i++)
            System.out.print(arr[i] + " ");
        System.out.println();
    }

    public static void main(String[] args) {
        int arr[] = {10, 7, 8, 9, 1, 5};
        quickSort(arr, 0, arr.length - 1);
        System.out.println("Sorted array: ");
        printArray(arr);
    }

    static int partition(int arr[], int low, int high) {
        int pivot = arr[high]; // pivot
        int i = (low - 1); // Index of smaller element

        for (int j = low; j <= high - 1; j++) {
            // If current element is smaller than or
            // equal to pivot
            if (arr[j] <= pivot) {
                i++; // increment index of smaller element
                int t = arr[i];
                arr[i] = arr[j];
                arr[j] = t;
            }
        }
        int t = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = t;
        return (i + 1);
    }
}
