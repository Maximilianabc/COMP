package castle.comp3021.assignment.protocol.io;

import castle.comp3021.assignment.player.RandomPlayer;
import castle.comp3021.assignment.protocol.*;
import castle.comp3021.assignment.protocol.exception.InvalidConfigurationError;
import castle.comp3021.assignment.protocol.exception.InvalidGameException;
import javafx.css.Match;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.*;
import java.util.stream.Collectors;

public class Deserializer {
    @NotNull
    private Path path;

    private Configuration configuration;

    private Integer[] storedScores;

    Place centralPlace;

    private ArrayList<MoveRecord> moveRecords = new ArrayList<>();



    public Deserializer(@NotNull final Path path) throws FileNotFoundException {
        if (!path.toFile().exists()) {
            throw new FileNotFoundException("Cannot find file to load!");
        }

        this.path = path;
    }

    /**
     * Returns the first non-empty and non-comment (starts with '#') line from the reader.
     *
     * @param br {@link BufferedReader} to read from.
     * @return First line that is a parsable line, or {@code null} there are no lines to read.
     * @throws IOException if the reader fails to read a line
     * @throws InvalidGameException if unexpected end of file
     */
    @Nullable
    private String getFirstNonEmptyLine(@NotNull final BufferedReader br) throws IOException {
        String line;
        while ((line = br.readLine()) != null) {
            if (!line.isBlank() && line.charAt(0) != '#') return line;
        }
        return null;
    }

    public void parseGame() {
        try (var reader = new BufferedReader(new FileReader(path.toFile()))) {
            String line;

            int size;
            line = getFirstNonEmptyLine(reader);
            if (line != null) {
                Matcher m = Pattern.compile("^size:(\\d+)").matcher(line);
                size = m.find() ? Integer.parseInt(m.group(1)) : -1;
            } else {
                throw new InvalidGameException("Unexpected EOF when parsing number of board size");
            }

            int numMovesProtection;
            line = getFirstNonEmptyLine(reader);
            if (line != null) {
                Matcher m = Pattern.compile("^numMovesProtection:(\\d+)").matcher(line);
                numMovesProtection = m.find() ? Integer.parseInt(m.group(1)) : -1;
            } else {
                throw new InvalidGameException("Unexpected EOF when parsing number of columns");
            }

            /**
             *  read central place here
             *  If success, assign to {@link Deserializer#centralPlace}
             *  Hint: You may use {@link Deserializer#parsePlace(String)}
             */
            line = getFirstNonEmptyLine(reader);
            Matcher ma = Pattern.compile("^centralPlace:\\((\\d+),(\\d+)\\)").matcher(line);
            centralPlace = ma.find() ? new Place(Integer.parseInt(ma.group(1)), Integer.parseInt(ma.group(2))) : null;

            int numPlayers;
            line = getFirstNonEmptyLine(reader);
            if (line != null) {
                Matcher m = Pattern.compile("^numPlayers:(\\d+)").matcher(line);
                numPlayers = m.find() ? Integer.parseInt(m.group(1)) : -1;
            } else {
                throw new InvalidGameException("Unexpected EOF when parsing number of players");
            }

            /**
             * create an array of players {@link Player} with length of numPlayers, and name it by the read-in name
             * Also create an array representing scores {@link Deserializer#storedScores} of players with length of numPlayers
             */
            List<Player> ps = new ArrayList<>();
            List<Integer> scores = new ArrayList<>();
            Pattern p = Pattern.compile("name:(\\w+); score:(\\d+)");
            while ((line = getFirstNonEmptyLine(reader)) != "END") {
                Matcher m = p.matcher(line);
                if (m.find()) {
                    ps.add(new RandomPlayer(m.group(1)));
                    scores.add(Integer.parseInt(m.group(2)));
                }
                else {
                    break;
                }
            }
            Player[] players = null;
            storedScores = null;
            if (numPlayers > 0) {
                players = ps.toArray(Player[]::new);
                storedScores = scores.toArray(Integer[]::new);
            }

            /**
             * try to initialize a configuration object  with the above read-in variables
             * if fail, throw InvalidConfigurationError exception
             * if success, assign to {@link Deserializer#configuration}
             */
            try {
                Configuration c = new Configuration(size, players, numMovesProtection);
                if (!c.getCentralPlace().equals(centralPlace)) {
                    throw new InvalidConfigurationError("Unexpected central place found.");
                }
                configuration = c;
            } catch (Exception e) {
                throw new InvalidConfigurationError(e.getMessage());
            }

            /**
             * Parse the string of move records into an array of {@link MoveRecord}
             * Assign to {@link Deserializer#moveRecords}
             * You should first implement the following methods:
             * - {@link Deserializer#parseMoveRecord(String)}}
             * - {@link Deserializer#parseMove(String)} ()}
             * - {@link Deserializer#parsePlace(String)} ()}
             */
            Pattern pt = Pattern.compile("^player:(\\w+); move:\\((\\d+),(\\d+)\\)->\\((\\d+),(\\d+)\\)");

            while (line != "END") {
                Matcher m = pt.matcher(line);
                if (m.find()) {
                    Player player = ps.stream().filter(a -> a.getName().equals(m.group(1))).findFirst().orElse(null);
                    if (player == null) {
                        throw new InvalidConfigurationError("Player " + m.group(1) + " is not present in player info session of history record.");
                    }
                    moveRecords.add(new MoveRecord(player, new Move(
                    Integer.parseInt(m.group(2)),
                    Integer.parseInt(m.group(3)),
                    Integer.parseInt(m.group(4)),
                    Integer.parseInt(m.group(5))
                    )));
                    line = getFirstNonEmptyLine(reader);
                } else {
                    break;
                }
            }

        } catch (IOException ioe) {
            throw new InvalidGameException(ioe);
        }
    }

    public Configuration getLoadedConfiguration(){
        return configuration;
    }

    public Integer[] getStoredScores(){
        return storedScores;
    }

    public ArrayList<MoveRecord> getMoveRecords(){
        return moveRecords;
    }

    /**
     * Parse the string into a {@link MoveRecord}
     * Handle InvalidConfigurationError if the parse fails.
     * @param moveRecordString a string of a move record
     * @return a {@link MoveRecord}
     */
    private MoveRecord parseMoveRecord(String moveRecordString){
        // TODO
        return null;
    }

    /**
     * Parse a string of move to a {@link Move}
     * Handle InvalidConfigurationError if the parse fails.
     * @param moveString given string
     * @return {@link Move}
     */
    private Move parseMove(String moveString) {
        // TODO
        return null;
    }

    /**
     * Parse a string of move to a {@link Place}
     * Handle InvalidConfigurationError if the parse fails.
     * @param placeString given string
     * @return {@link Place}
     */
    private Place parsePlace(String placeString) {
        //TODO
        return null;
    }


}
