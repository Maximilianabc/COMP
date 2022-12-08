package comp3111.popnames;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class NamePopularityTest{

    @Test
    public void testIncorrectGender() {
        String oReport = NamePopularity.getSummary("Elsa","Male", 1950, 1959);
        assertEquals(oReport, "Error: name found but gender mismatch in year 1950");
    }

    @Test
    public void testIncorrectName() {
        String oReport = NamePopularity.getSummary("hello","Male", 1950, 1959);
        assertEquals(oReport, "Error: name not found in year 1950.");
    }

    @Test
    public void testYoungAgeMale(){
        String oReport = NamePopularity.getSummary("Edward","Male", 1880, 1889);
        assertEquals(oReport, "Report 2\n" +
                "In the year of 1889, the number of birth with name Edward is 2299, which represents 2.07896 percent of total Male births in 1889. The year when the name Edward was most popular is 1889. In that year, the number of births is 2299, which represents 2.07896 percent of the total Male birth in 1889.\n" +
                "\n" +
                "Year\t\t|\t\tRank\t|\t\tCount\t\t|\t\tPercentage\n" +
                "1889\t\t|\t\t10\t\t|\t\t2299\t\t|\t\t2.07896\n" +
                "1888\t\t|\t\t11\t\t|\t\t2470\t\t|\t\t2.04381\n" +
                "1887\t\t|\t\t10\t\t|\t\t2125\t\t|\t\t2.09537\n" +
                "1886\t\t|\t\t11\t\t|\t\t2312\t\t|\t\t2.08694\n" +
                "1885\t\t|\t\t11\t\t|\t\t2220\t\t|\t\t2.05937\n" +
                "1884\t\t|\t\t11\t\t|\t\t2439\t\t|\t\t2.13115\n" +
                "1883\t\t|\t\t11\t\t|\t\t2250\t\t|\t\t2.15046\n" +
                "1882\t\t|\t\t11\t\t|\t\t2477\t\t|\t\t2.17877\n" +
                "1881\t\t|\t\t10\t\t|\t\t2177\t\t|\t\t2.16090\n" +
                "1880\t\t|\t\t11\t\t|\t\t2364\t\t|\t\t2.13954\n");
    }

    @Test
    public void testMiddleAgeMale(){
        String oReport = NamePopularity.getSummary("Edward","Male", 1950, 1959);
        assertEquals(oReport, "Report 2\n" +
                "In the year of 1959, the number of birth with name Edward is 16911, which represents 0.792638 percent of total Male births in 1959. The year when the name Edward was most popular is 1954. In that year, the number of births is 19509, which represents 0.957556 percent of the total Male birth in 1954.\n" +
                "\n" +
                "Year\t\t|\t\tRank\t|\t\tCount\t\t|\t\tPercentage\n" +
                "1959\t\t|\t\t27\t\t|\t\t16911\t\t|\t\t0.792638\n" +
                "1958\t\t|\t\t28\t\t|\t\t17368\t\t|\t\t0.818970\n" +
                "1957\t\t|\t\t25\t\t|\t\t18702\t\t|\t\t0.867494\n" +
                "1956\t\t|\t\t25\t\t|\t\t19417\t\t|\t\t0.918629\n" +
                "1955\t\t|\t\t25\t\t|\t\t19256\t\t|\t\t0.935703\n" +
                "1954\t\t|\t\t22\t\t|\t\t19509\t\t|\t\t0.957556\n" +
                "1953\t\t|\t\t23\t\t|\t\t18956\t\t|\t\t0.962342\n" +
                "1952\t\t|\t\t23\t\t|\t\t19526\t\t|\t\t1.00428\n" +
                "1951\t\t|\t\t23\t\t|\t\t19879\t\t|\t\t1.05679\n" +
                "1950\t\t|\t\t22\t\t|\t\t18725\t\t|\t\t1.04570\n");
    }

    @Test
    public void testOldAgeMale(){
        String oReport = NamePopularity.getSummary("Edward","Male", 2010, 2019);
        assertEquals(oReport, "Report 2\n" +
                "In the year of 2019, the number of birth with name Edward is 2037, which represents 0.114442 percent of total Male births in 2019. The year when the name Edward was most popular is 2010. In that year, the number of births is 2898, which represents 0.151422 percent of the total Male birth in 2010.\n" +
                "\n" +
                "Year\t\t|\t\tRank\t|\t\tCount\t\t|\t\tPercentage\n" +
                "2019\t\t|\t\t190\t\t|\t\t2037\t\t|\t\t0.114442\n" +
                "2018\t\t|\t\t169\t\t|\t\t2280\t\t|\t\t0.126025\n" +
                "2017\t\t|\t\t166\t\t|\t\t2323\t\t|\t\t0.125876\n" +
                "2016\t\t|\t\t162\t\t|\t\t2510\t\t|\t\t0.132561\n" +
                "2015\t\t|\t\t159\t\t|\t\t2598\t\t|\t\t0.135803\n" +
                "2014\t\t|\t\t158\t\t|\t\t2567\t\t|\t\t0.135007\n" +
                "2013\t\t|\t\t145\t\t|\t\t2693\t\t|\t\t0.143133\n" +
                "2012\t\t|\t\t155\t\t|\t\t2588\t\t|\t\t0.136974\n" +
                "2011\t\t|\t\t146\t\t|\t\t2659\t\t|\t\t0.140448\n" +
                "2010\t\t|\t\t132\t\t|\t\t2898\t\t|\t\t0.151422\n");
    }

    @Test
    public void testYoungAgeFemale(){
        String oReport = NamePopularity.getSummary("Grace","Female", 1880, 1889);
        assertEquals(oReport, "Report 2\n" +
                "In the year of 1889, the number of birth with name Grace is 2049, which represents 1.14876 percent of total Female births in 1889. The year when the name Grace was most popular is 1884. In that year, the number of births is 1525, which represents 1.18197 percent of the total Female birth in 1884.\n" +
                "\n" +
                "Year\t\t|\t\tRank\t|\t\tCount\t\t|\t\tPercentage\n" +
                "1889\t\t|\t\t15\t\t|\t\t2049\t\t|\t\t1.14876\n" +
                "1888\t\t|\t\t15\t\t|\t\t2090\t\t|\t\t1.17004\n" +
                "1887\t\t|\t\t15\t\t|\t\t1692\t\t|\t\t1.15905\n" +
                "1886\t\t|\t\t14\t\t|\t\t1723\t\t|\t\t1.19210\n" +
                "1885\t\t|\t\t14\t\t|\t\t1575\t\t|\t\t1.18372\n" +
                "1884\t\t|\t\t13\t\t|\t\t1525\t\t|\t\t1.18197\n" +
                "1883\t\t|\t\t13\t\t|\t\t1307\t\t|\t\t1.16363\n" +
                "1882\t\t|\t\t16\t\t|\t\t1195\t\t|\t\t1.10802\n" +
                "1881\t\t|\t\t15\t\t|\t\t1089\t\t|\t\t1.18429\n" +
                "1880\t\t|\t\t19\t\t|\t\t982\t\t|\t\t1.07920\n");
    }

    @Test
    public void testMiddleAgeFemale(){
        String oReport = NamePopularity.getSummary("Grace","Female", 1950, 1959);
        assertEquals(oReport, "Report 2\n" +
                "In the year of 1959, the number of birth with name Grace is 1660, which represents 0.0820546 percent of total Female births in 1959. The year when the name Grace was most popular is 1951. In that year, the number of births is 1578, which represents 0.0876646 percent of the total Female birth in 1951.\n" +
                "\n" +
                "Year\t\t|\t\tRank\t|\t\tCount\t\t|\t\tPercentage\n" +
                "1959\t\t|\t\t201\t\t|\t\t1660\t\t|\t\t0.0820546\n" +
                "1958\t\t|\t\t194\t\t|\t\t1706\t\t|\t\t0.0848383\n" +
                "1957\t\t|\t\t186\t\t|\t\t1918\t\t|\t\t0.0938283\n" +
                "1956\t\t|\t\t188\t\t|\t\t1837\t\t|\t\t0.0915063\n" +
                "1955\t\t|\t\t215\t\t|\t\t1387\t\t|\t\t0.0709585\n" +
                "1954\t\t|\t\t211\t\t|\t\t1410\t\t|\t\t0.0726175\n" +
                "1953\t\t|\t\t205\t\t|\t\t1380\t\t|\t\t0.0733915\n" +
                "1952\t\t|\t\t190\t\t|\t\t1524\t\t|\t\t0.0821697\n" +
                "1951\t\t|\t\t181\t\t|\t\t1578\t\t|\t\t0.0876646\n" +
                "1950\t\t|\t\t181\t\t|\t\t1510\t\t|\t\t0.0881402\n");
    }

    @Test
    public void testOldAgeFemale(){
        String oReport = NamePopularity.getSummary("Grace","Female", 2010, 2019);
        assertEquals(oReport, "Report 2\n" +
                "In the year of 2019, the number of birth with name Grace is 6062, which represents 0.364003 percent of total Female births in 2019. The year when the name Grace was most popular is 2011. In that year, the number of births is 7622, which represents 0.434674 percent of the total Female birth in 2011.\n" +
                "\n" +
                "Year\t\t|\t\tRank\t|\t\tCount\t\t|\t\tPercentage\n" +
                "2019\t\t|\t\t28\t\t|\t\t6062\t\t|\t\t0.364003\n" +
                "2018\t\t|\t\t24\t\t|\t\t6758\t\t|\t\t0.398787\n" +
                "2017\t\t|\t\t21\t\t|\t\t7054\t\t|\t\t0.409747\n" +
                "2016\t\t|\t\t19\t\t|\t\t7603\t\t|\t\t0.430058\n" +
                "2015\t\t|\t\t19\t\t|\t\t7665\t\t|\t\t0.430201\n" +
                "2014\t\t|\t\t21\t\t|\t\t7554\t\t|\t\t0.427075\n" +
                "2013\t\t|\t\t22\t\t|\t\t7345\t\t|\t\t0.420835\n" +
                "2012\t\t|\t\t21\t\t|\t\t7359\t\t|\t\t0.419574\n" +
                "2011\t\t|\t\t16\t\t|\t\t7622\t\t|\t\t0.434674\n" +
                "2010\t\t|\t\t18\t\t|\t\t7675\t\t|\t\t0.432946\n");
    }

}