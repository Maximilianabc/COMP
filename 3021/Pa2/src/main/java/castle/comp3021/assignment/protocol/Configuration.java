package castle.comp3021.assignment.protocol;

import castle.comp3021.assignment.piece.Archer;
import castle.comp3021.assignment.piece.Knight;
import castle.comp3021.assignment.player.ConsolePlayer;
import castle.comp3021.assignment.player.RandomPlayer;
import castle.comp3021.assignment.protocol.exception.InvalidConfigurationError;


/**
 * Game configuration, including:
 * 1. size of gameboard
 * 2. place (square) of central square in classical Jeson Mor
 * 3. the initial game board with pieces on it, which is configurable through {@link Configuration#addInitialPiece(Piece, Place)}
 * 4. the two players.
 */
public class Configuration implements Cloneable {
    /**
     * Set default size to 9.
     * Set default numMovesProtection to 3
     */
    protected final static int DEFAULTSIZE = 9;
    protected final static int DEFAULTPROTECTMOVE = 1;

    /**
     * Size of game board.
     * The game board has equal size in width and height.
     * The size should be an odd number.
     */
    protected int size;

    /**
     * All players in the game.
     */
    protected Player[] players;

    /**
     * The initial map of the gameboard, containing initial pieces and their places.
     * This map has keys for all places in the gameboard, with or without pieces.
     * If there is no piece in one place, the place is mapped to null.
     */
    protected Piece[][] initialBoard;

    /**
     * The central square of the game board.
     */
    protected Place centralPlace;

    protected int numMovesProtection;

    /**
     * Constructor of configuration
     *
     * @param size size of the game board.
     */
    public Configuration(int size, Player[] players, int numMovesProtection) {
        // validate size
        if (size < 3) {
            throw new InvalidConfigurationError("size of gameboard must be at least 3");
        }
        if (size % 2 != 1) {
            throw new InvalidConfigurationError("size of gameboard must be an odd number");
        }
        if (size > 26) {
            throw new InvalidConfigurationError("size of gameboard is at most 26");
        }

        if (numMovesProtection < 0) {
            throw new InvalidConfigurationError("number of moves with capture protection cannot be negative");
        }

        this.size = size;
        // We only have 2 players
        this.players = players;

        this.numMovesProtection = numMovesProtection;

        if (players.length != 2) {
            throw new InvalidConfigurationError("there must be exactly two players");
        }
        // initialize map of the game board by putting every place null (meaning no piece)
        this.initialBoard = new Piece[size][];
        for (int x = 0; x < size; x++) {
            this.initialBoard[x] = new Piece[size];
            for (int y = 0; y < size; y++) {
                this.initialBoard[x][y] = null;
            }
        }
        // calculate the central place
        this.centralPlace = new Place(size / 2, size / 2);
    }

    public Configuration(int size, Player[] players) {
        this(size, players, 0);
    }

    public Configuration(Player[] players) {
        this(Configuration.DEFAULTSIZE, players, Configuration.DEFAULTPROTECTMOVE);
    }

    public Configuration() {
        this.size = DEFAULTSIZE;
        this.numMovesProtection = DEFAULTPROTECTMOVE;
        Player whitePlayer = new RandomPlayer("White");
        Player blackPlayer = new RandomPlayer("Black");
        this.players = new Player[]{whitePlayer, blackPlayer};
    }

    /**
     * Add piece to the initial gameboard.
     * The player that this piece belongs to will be automatically added into the configuration.
     *
     * @param piece piece to be added
     * @param place place to put the piece
     */
    public void addInitialPiece(Piece piece, Place place) {
        if (!piece.getPlayer().equals(this.players[0]) && !piece.getPlayer().equals(this.players[1])) {
            throw new InvalidConfigurationError("the player of the piece is unknown");
        }
        if (place.x() >= this.size || place.y() >= this.size) {
            // The place must be inside the game board
            throw new InvalidConfigurationError("the place" + place.toString() + " must be inside the game board");
        }
        if (place.equals(this.centralPlace)) {
            throw new InvalidConfigurationError("piece cannot be put at central place initially");
        }

        // put the piece on the initial board
        this.initialBoard[place.x()][place.y()] = piece;
    }

    public void addInitialPiece(Piece piece, int x, int y) {
        this.addInitialPiece(piece, new Place(x, y));
    }

    public int getSize() {
        return size;
    }

    public Player[] getPlayers() {
        return players;
    }

    public Piece[][] getInitialBoard() {
        return initialBoard;
    }

    public Place getCentralPlace() {
        return centralPlace;
    }

    public int getNumMovesProtection() {
        return numMovesProtection;
    }

    @Override
    public Configuration clone() throws CloneNotSupportedException {
        var cloned = (Configuration) super.clone();
        cloned.players = this.players.clone();
        for (int i = 0; i < this.players.length; i++) {
            cloned.players[i] = this.players[i].clone();
        }
        cloned.initialBoard = this.initialBoard.clone();
        for (int i = 0; i < this.size; i++) {
            cloned.initialBoard[i] = this.initialBoard[i].clone();
            // no need to deep copy piece
            System.arraycopy(this.initialBoard[i], 0, cloned.initialBoard[i], 0, this.size);
        }
        cloned.centralPlace = this.centralPlace.clone();
        return cloned;
    }

    public void setSize(int size) {
        this.size = size;
    }

    public void setNumMovesProtection(int numMovesProtection){
        this.numMovesProtection = numMovesProtection;
    }

    /**
     * Check whether the first player is human player or not
     * @return boolean
     */
    public boolean isFirstPlayerHuman(){
        return players[0].getClass().getName().contains("ConsolePlayer");
    }

    /**
     * Check whether the second player is human player or not
     * @return boolean
     */
    public boolean isSecondPlayerHuman(){
        return players[1].getClass().getName().contains("ConsolePlayer");
    }

    /**
     * set the first player to be human or computer
     * @param isHuman whether the first is human or not
     */
    public void setFirstPlayerHuman(boolean isHuman){
        players[0] = isHuman ? new ConsolePlayer("White") : new RandomPlayer("White");
    }

    /**
     * set the second player to be human or computer
     * @param isHuman whether the second is human or not
     */
    public void setSecondPlayerHuman(boolean isHuman){
        players[1] = isHuman ? new ConsolePlayer("Black") : new RandomPlayer("Black");
    }


    /**
     * set initial pieces
     */
    public void setAllInitialPieces(){
        Player whitePlayer = this.getPlayers()[1];
        Player blackPlayer = this.getPlayers()[0];

        for (int i = 0; i < size; i++) {
            if (i % 2 == 0) {
                this.addInitialPiece(new Knight(whitePlayer), i, size - 1);
            } else {
                this.addInitialPiece(new Archer(whitePlayer), i, size - 1);
            }
        }
        for (int i = 0; i < size; i++) {
            if (i % 2 == 0) {
                this.addInitialPiece(new Knight(blackPlayer), i, 0);
            } else {
                this.addInitialPiece(new Archer(blackPlayer), i, 0);
            }
        }
    }

    /**
     * Write configuration to string
     *
     * - Format example:
     * size:9
     * numMovesProtection:1
     * centralPlace:(4,4)
     * numPlayers:2
     *
     * #Player info
     * #player1:
     * name:White, score:14
     * #player2:
     * name:Black, score:26
     *
     * @return the string of configuration
     */
    @Override
    public String toString() {
        return String.format("size:%1$d\nnumMovesProtection:%2$d\ncentralPlace:%3$s\nnumPlayers:%4$d\n\n"
        + "#Player info\n#player1:\nname:%5$s, score:%6$d\n#player2:\nname:%7$s, score:%8$d\n",
        size, numMovesProtection, centralPlace.toString(), players.length, players[0].getName(),
        players[0].getScore(), players[1].getName(), players[1].getScore());
    }
}
