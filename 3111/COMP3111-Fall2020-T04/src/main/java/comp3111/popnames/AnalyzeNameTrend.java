package comp3111.popnames;
import java.util.ArrayList;
import java.util.Collections;

/** 
 * A class for computing the result for task 3.
 * @author Maisy
 */

public class AnalyzeNameTrend {
	/**
	 * A private class for task 3. Every instances represent a row in task 3's result.
	 */
	private class NameTrend{
		/** The name of this record. */
		public String name;
		/** The highest rank the name every got in the given period. */
		public int highest_rank;
		/** The year corresponding to the highest rank. */
		public int highest_rank_year;
		/** The lowest rank (value of highest_rank < value of lowest_rank) the name every got in the given period. */
		public int lowest_rank;
		/** The year corresponding to the lowest rank. */
		public int lowest_rank_year;
		/** A counter to count the name exist in how many year in the given period as Top N. */
		public int exist_in_num_years = 1;
		/** The difference between highest_rank and lowest_rank. */
		public int max_rank_diff = 0;

		/** The constructor. */
		public NameTrend(String name, int rank, int year){
			this.name = name;
			this.highest_rank = this.lowest_rank = rank;
			this.highest_rank_year = this.lowest_rank_year = year;
		}

		/**
		 * Update the highest / lowest rank (and corresponding year), given a new rank 
		 * of the name (that has not used to update the rank before).
		 * @param new_rank The new rank that maybe used to update highest_rank/lowest_rank.
		 * @param new_rank_year The new year that maybe used to update highest_rank_year/lowest_rank_year.
		 */
		public void updateRank(int new_rank, int new_rank_year){
			if (new_rank < highest_rank){
				highest_rank = new_rank;
				highest_rank_year = new_rank_year;
			} else if (new_rank > lowest_rank){
				lowest_rank = new_rank;
				lowest_rank_year = new_rank_year;
			}
			exist_in_num_years++;
		}

		/** Compute the difference between highest_rank and lowest_rank */
		public void calculateMaxRandDiff(){
			max_rank_diff = lowest_rank - highest_rank;
		}

		/** Generate a text description of this record.
		 * @return A string in form of "FLAT" / "UP for %d" / "DOWN for %d".
		 */
		public String getTrendDescription(){
			if (lowest_rank_year == highest_rank_year){
				return String.format("FLAT");
			}
			if (lowest_rank_year < highest_rank_year){
				return String.format("UP for %d", max_rank_diff);
			}
			return String.format("DOWN for %d", max_rank_diff);
		}
	}
	
	// Take those input (has been converted into suitable type) as properties 
	// so that I don't have to pass them again and again among function.

	/** The start of the given period. */
	private int start_year;
	/** The end of the given period. */
	private int end_year;
	/** The given gender the names should be correspond to. */
	private Gender gender;
	/** The given range of rank (top N) to search with. */
	private int top_N;
	/** The error message from the inputValidation() (if any). */
	private String error_message = "";
	/** A list to store all records that should be in the result/output. */
	private ArrayList<NameTrend> result = new ArrayList<NameTrend>();

	/**
	 * The private constructor which is only called by ComputeAndGetResult().
	 */
	private AnalyzeNameTrend(String start_year, String end_year, String gender, String top_N){
		inputValidationAndConversion(start_year, end_year, gender, top_N);
		this.gender = Gender.convertFromString(gender);
	}
	
	/**
	 * The only public method in this class. Called by Controller.
	 * Get all input value and call the other method to <br>
	 * - Do validation. <br>
	 * - Compute result. <br>
	 * - Phrase the result into a user-friendly output.
	 * 
	 * @return The error message or the result string to be placed on the UI as output.
	 */
	public static String ComputeAndGetResult(String start_year, String end_year, String gender, String top_N){
		AnalyzeNameTrend name_trend = new AnalyzeNameTrend(start_year, end_year, gender, top_N);
		
		if (!name_trend.error_message.equals("")){ // if has error in input
			return name_trend.error_message;
		}
		name_trend.computeTrend();
		
		return name_trend.phraseOutput();
	}

	/**
	 * To check if all input are valid (input with minor mistake are also accepted after modified).
	 * Set the related properties that may accepted modified input.
	 * Set the error message (if any).
	 */
	private void inputValidationAndConversion(String start_year, String end_year, String gender, String top_N){
		String alter_input;
		// year
		alter_input = start_year.trim();
		if (!DataValidation.isValidYearString(alter_input)){
			error_message = "Error for start year: " + DataValidation.invalidYearString;
			return;
		}
		this.start_year = Integer.parseInt(alter_input);
		
		alter_input = end_year.trim();
		if (!DataValidation.isValidYearString(alter_input)){
			error_message = "Error for end year: " + DataValidation.invalidYearString;
			return;
		}
		this.end_year = Integer.parseInt(alter_input);

		if (!DataValidation.checkPeriod(this.start_year, this.end_year)){
			error_message = "Error for period: " + DataValidation.invalidPeriod;
			return;
		}

		if (this.start_year == this.end_year){
			error_message = "Error for period: Please input a period of at least two year.";
			return;
		}

		// top_N
		alter_input = top_N.trim();
		if (!DataValidation.isValidIntString(alter_input)){
			error_message = "Error for top N: " + DataValidation.invalidIntString;
			return;
		}
		this.top_N = Integer.parseInt(alter_input);
		if (this.top_N <= 0){
			error_message = "Error for top N: Please input a integer > 0.";
			return;
		}

	}

	/**
	 * Find and "fill up" all records that should included in the result.
	 */
	@SuppressWarnings("unchecked")
	private void computeTrend(){
		// get all record in the period with given gender and rank in top N.
		int num_years_in_period = end_year - start_year + 1;
		ArrayList<DataRecord>[] all_years_records = new ArrayList[num_years_in_period];
		for (int i = 0, this_year = start_year; this_year <= end_year; i++, this_year++){
			all_years_records[i] = DataParser.getRecordOfTopN(this_year, gender, top_N);
			// sort the records according to name
			Collections.sort(all_years_records[i]);
		}
				
		// find out the trend by the concept of merge sort

		// Create a list of "pointers" that point to the fist un-visit records in each year records list.
		int[] indexes = new int[num_years_in_period];
		for (int i = 0; i < num_years_in_period; i++){
			indexes[i] = 0;
		}

		// for each name appear in the first year top N list
		for (DataRecord this_first_year_record : all_years_records[0]){
			NameTrend this_name_trend = new NameTrend(this_first_year_record.name, this_first_year_record.rank, start_year);
			
			// for the rest of the year
			// see if it exists in that year
			// if yes, update the result record this_name_trend
			for (int i = 1, this_year = start_year+1; this_year <= end_year; i++, this_year++){
				int compare;
				do {
					// if reach the end of the all_years_records[i]
					if (indexes[i] >= all_years_records[i].size()){ 
						break;
					}

					DataRecord compare_to_record = all_years_records[i].get(indexes[i]);
					compare = compare_to_record.compareTo(this_first_year_record);
					
					if (compare == 0){ // same name
						// calculate the trend
						this_name_trend.updateRank(compare_to_record.rank, this_year);
						// increase the index by 1 (for next name)
						indexes[i]++;
					} else if (compare < 0 ){ // "smaller", don't appear in 1st year
						// increase the index by 1 (for next name)
						indexes[i]++;
					}

				} while (compare <= 0); // until reach a "larger" name for next this_first_year_record
			}

			// only include this_name_trend in the final result if the name appear in every year's top N list
			if (this_name_trend.exist_in_num_years == num_years_in_period){
				this.result.add(this_name_trend);
			}
		}
	
		// calculate the rank diff of all records that in the result
		for (NameTrend nt : result){
			nt.calculateMaxRandDiff();
		}
	}

	/**
	 * Phrase the result into a user-friendly output.
	 * @return The result string.
	 */
	private String phraseOutput(){
		// return String.format("Hi from task 3");
		//"k names are found to be maintained at a high level of popularity within Top N 
		// over the period from year y1 to year y2."
		String final_result = String.format("%d names are found to be maintained at a high level of popularity within Top %d over the period from year %d to year %d.\n\n", result.size(), top_N, start_year, end_year);

		// Some strings for formatting the result
		String line = "----------------------------------------------------------------------------------\n";
		String format_title = "| %-10s\t | %-15s\t | %-15s\t | %-10s\t |\n";
		String format = "| %-10s\t | %6s%-6d%5d\t | %5s%-6d%5d\t | %-10s\t |\n";
		
		// title
		final_result += line;
		final_result += String.format(format_title, "Name", "Highest Rank[Year]", "Lowest Rank[Year]", "Gross Trend");
		final_result += line;
		
		// records
		for (NameTrend name_trend : result){
			final_result += String.format(format, name_trend.name,"", name_trend.highest_rank, name_trend.highest_rank_year, "", name_trend.lowest_rank, name_trend.lowest_rank_year, name_trend.getTrendDescription());
		}
		final_result += line;

		return final_result;
	}
}
