package comp3111.popnames;

import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.text.Font;

import java.io.IOException;

/**
 * Building on the sample skeleton for 'ui.fxml' COntroller Class generated by SceneBuilder
 */

public class Controller {

    @FXML
    private Tab tabTaskZero;

    @FXML
    private TextField textfieldNameF;

    @FXML
    private TextField textfieldYear;

    @FXML
    private Button buttonRankM;

    @FXML
    private TextField textfieldNameM;

    @FXML
    private Button buttonRankF;

    @FXML
    private Button buttonTopM;

    @FXML
    private Button buttonTopF;

    @FXML
    private Button buttonSummary;

    @FXML
    private Tab tabReport1;

    @FXML
    private ToggleGroup T1;

    @FXML
    private Tab tabReport2;

    @FXML
    private ToggleGroup T11;
    /** Task 1 */
    @FXML
    private TextField t1TopN;
    @FXML
    private TextField t1Y1;
    @FXML
    private TextField t1Y2;
    /** Task 2 */
    @FXML
    public TextField textfieldNameT2;
    @FXML
    public TextField textfieldY1T2;
    @FXML
    public TextField textfieldY2T2;
    /** End of Task 2 */

    /** Task 3: Reporting 3 */
    @FXML
    private Tab tabReport3;

    @FXML
    private RadioButton report3_Female;

    @FXML
    private ToggleGroup Report3gender;

    @FXML
    private RadioButton report3_Male;

    @FXML
    private TextField report3_end_year;

    @FXML
    private TextField report3_start_year;
    
    @FXML
    private TextField report3_top_N;

    @FXML
    private Button report3_start;
    /** End of task 3 ui elements */

    @FXML
    private Tab tabApp1;

    /** Task 4: Application 1*/
    @FXML
    private TextField dadName;

    @FXML
    private TextField momName;

    @FXML
    private TextField dadYOB;

    @FXML
    private TextField momYOB;

    @FXML
    private TextField vintageYear;

    /** Task 5: Application 2 */
    @FXML
    private Tab tabApp2;

    @FXML
    private TextField app2_user_name;

    @FXML
    private ToggleGroup app2_your_gender;

    @FXML
    private TextField app2_user_YOB;

    @FXML
    private ToggleGroup app2_soulmate_gender;

    @FXML
    private ToggleGroup app2_soulmate_age;
    /** End of Task 5: Application 2 */

    /** Task 6: Application 3 */
    @FXML
    private Tab tabApp3;

    @FXML
    private TextField app3_soulmate_name;

    @FXML
    private TextField app3_user_name;

    @FXML
    private RadioButton app3_user_male;

    @FXML
    private ToggleGroup App3yourGender;

    @FXML
    private RadioButton app3_soulmate_younger;

    @FXML
    private ToggleGroup App3age;

    @FXML
    private RadioButton app3_soulmate_older;

    @FXML
    private RadioButton app3_user_female;

    @FXML
    private TextField app3_user_YOB;

    @FXML
    private RadioButton app3_soulmate_male;

    @FXML
    private ToggleGroup App3soulmateGender;

    @FXML
    private RadioButton app3_soulmate_female;

    @FXML
    private Button app3_start;
    /** End of task 6 ui element */

    @FXML
    private TextArea textAreaConsole;
    

    /**
     *  Task Zero
     *  To be triggered by the "Summary" button on the Task Zero Tab 
     *  
     */
    @FXML
    void doSummary() {
    	int year = Integer.parseInt(textfieldYear.getText());
    	String oReport = AnalyzeNames.getSummary(year);
    	textAreaConsole.setText(oReport);
    }

  
    /**
     *  Task Zero
     *  To be triggered by the "Rank (female)" button on the Task Zero Tab
     *  
     */
    @FXML
    void doRankF() {
    	String oReport = "";
    	String iNameF = textfieldNameF.getText();
    	int iYear = Integer.parseInt(textfieldYear.getText());
    	int oRank = AnalyzeNames.getRank(iYear, iNameF, "F");
    	if (oRank == -1)
    		oReport = String.format("The name %s (female) has not been ranked in the year %d.\n", iNameF, iYear);
    	else
    		oReport = String.format("Rank of %s (female) in year %d is #%d.\n", iNameF, iYear, oRank);
    	textAreaConsole.setText(oReport);
    }

  
    /**
     *  Task Zero
     *  To be triggered by the "Rank (male)" button on the Task Zero Tab
     *  
     */
    @FXML
    void doRankM() {
    	String oReport = "";
    	String iNameM = textfieldNameM.getText();
    	int iYear = Integer.parseInt(textfieldYear.getText());
    	int oRank = AnalyzeNames.getRank(iYear, iNameM, "M");
    	if (oRank == -1)
    		oReport = String.format("The name %s (male) has not been ranked in the year %d.\n", iNameM, iYear);
    	else
    		oReport = String.format("Rank of %s (male) in year %d is #%d.\n", iNameM, iYear, oRank);
    	textAreaConsole.setText(oReport);
    }


    /**
     *  Task Zero
     *  To be triggered by the "Top 5 (female)" button on the Task Zero Tab
     *  
     */
    @FXML
    void doTopF() {
    	String oReport = "";
    	final int topN = 5;
    	int iYear = Integer.parseInt(textfieldYear.getText());
    	oReport = String.format("Top %d most popular names (female) in the year %d:\n", topN, iYear);
    	for (int i=1; i<=topN; i++)
    		oReport += String.format("#%d: %s\n", i, AnalyzeNames.getName(iYear, i, "F"));
    	textAreaConsole.setText(oReport);
    }


    /**
     *  Task Zero
     *  To be triggered by the "Top 5 (male)" button on the Task Zero Tab
     *  
     */
    @FXML
    void doTopM() {
    	String oReport = "";
    	final int topN = 5;
    	int iYear = Integer.parseInt(textfieldYear.getText());
    	oReport = String.format("Top %d most popular names (male) in the year %d:\n", topN, iYear);
    	for (int i=1; i<=topN; i++)
    		oReport += String.format("#%d: %s\n", i, AnalyzeNames.getName(iYear, i, "M"));
    	textAreaConsole.setText(oReport);
    }

    @FXML
    void resetConsole() {
    	textAreaConsole.setFont(Font.font("System"));
    	textAreaConsole.setText("");
    	textAreaConsole.setWrapText(true);
    }

    /** Task One
     *  To be triggered by the Report button on the Task One Tab
     */
    @FXML
    void doReportT1() throws IOException
    {
        resetConsole();
        String gender = ((RadioButton)T1.getSelectedToggle()).getText();
        String t1y1 = t1Y1.getText();
        String t1y2 = t1Y2.getText();
        String t1top = t1TopN.getText();

        textAreaConsole.setWrapText(false);
        textAreaConsole.setFont(Font.font("Courier New"));
        textAreaConsole.setText(TopNamesForBirth.Compute(t1y1, t1y2, t1top, Gender.fromString(gender)));
    }

    /**
     *  Task Two
     *  To be triggered by the Report button on the Task Two Tab
     *
     */
    @FXML
    void doReportT2(){
        String oReport = "";
        String iName = textfieldNameT2.getText();
        String iGender = ((RadioButton)T11.getSelectedToggle()).getText();
        String iY1 = textfieldY1T2.getText();
        String iY2 = textfieldY2T2.getText();
        //input validation
        if(!DataValidation.isValidName(iName)){
            textAreaConsole.setText(DataValidation.invalidName);
            return;
        }
        if(!DataValidation.isValidYearString(iY1) || !DataValidation.isValidYearString(iY2)){
            textAreaConsole.setText(DataValidation.invalidYearString);
            return;
        }
        int iYear1 = Integer.parseInt(iY1);
        int iYear2 = Integer.parseInt(iY2);
        if(!DataValidation.checkYearInRange(iYear1) || !DataValidation.checkYearInRange(iYear2)){
            textAreaConsole.setText(DataValidation.yearNotInRange);
            return;
        }
        if(!DataValidation.checkPeriod(iYear1, iYear2)){
            textAreaConsole.setText(DataValidation.invalidPeriod);
            return;
        }
        oReport = NamePopularity.getSummary(iName, iGender, iYear1, iYear2);
        textAreaConsole.setText(oReport);
    }
    
    /**
     * The controller for Task 3. <br>
     * Get the input from the ui elements. Call the "processor" to compute result.
     * And show the result on the ui.
     */
    @FXML
    void startReport3() {
        //textAreaConsole.setText("task 3");
        // get text box input
        String start_year = report3_start_year.getText();
        String end_year = report3_end_year.getText();
        String top_N = report3_top_N.getText();

        // get radio button selected
        RadioButton selected = (RadioButton)Report3gender.getSelectedToggle();
        String gender = selected.getText();

        // get result and output it
        String result = AnalyzeNameTrend.ComputeAndGetResult(start_year, end_year, gender, top_N);
        textAreaConsole.setFont(Font.font("Courier"));
        textAreaConsole.setText(result);
    }

    /**
     * Task 4
     */
    @FXML
    void startApplication1() {
        String d = dadName.getText();
        String m = momName.getText();
        String dyob = dadYOB.getText();
        String myob = momYOB.getText();
        String v = vintageYear.getText();
        textAreaConsole.setText(NamesRecommendation.recommend(d, m, dyob, myob, v));
    }

    /**
     * Task 5
     */
    @FXML
    void startApplication2() {
        String oReport = "";
        String iName = app2_user_name.getText();
        String iGender = ((RadioButton)app2_your_gender.getSelectedToggle()).getText();
        String iMateGender = ((RadioButton)app2_soulmate_gender.getSelectedToggle()).getText();
        String iPreference = ((RadioButton)app2_soulmate_age.getSelectedToggle()).getText();
        String iY = app2_user_YOB.getText();
        //input validation
        if(!DataValidation.isValidName(iName)){
            textAreaConsole.setText(DataValidation.invalidName);
            return;
        }
        if(!DataValidation.isValidYearString(iY)){
            textAreaConsole.setText(DataValidation.invalidYearString);
            return;
        }
        int iYOB = Integer.parseInt(iY);
        if(!DataValidation.checkYearInRange(iYOB)){
            textAreaConsole.setText(DataValidation.yearNotInRange);
            return;
        }
        oReport = PairNamePrediction.predictName(iName, iGender, iYOB, iMateGender, iPreference);
        textAreaConsole.setText(oReport);
    }


    /**
     * The controller for Task 6. <br>
     * Get the input from the ui elements. Call the "processor" to compute result.
     * And show the result on the ui.
     */
    @FXML
    void startApplication3() {
        // textAreaConsole.setText("task 6");

        // get text box input
        String user_name = app3_user_name.getText();
        String user_YOB = app3_user_YOB.getText();
        String soulmate_name = app3_soulmate_name.getText();

        // get radio button selected
        RadioButton selected;

        selected = (RadioButton)App3yourGender.getSelectedToggle();
        String user_gender = selected.getText();

        selected = (RadioButton)App3soulmateGender.getSelectedToggle();
        String soulmate_gender = selected.getText();

        selected = (RadioButton)App3age.getSelectedToggle();
        String soulmate_age = selected.getText();

        // get result and output it
        String result = PredictCompatibleScore.ComputeAndGetResult(user_name, user_YOB, soulmate_name, user_gender, soulmate_gender, soulmate_age);
        textAreaConsole.setText(result);
    }


}

