package castle.comp3021.assignment;

import castle.comp3021.assignment.mock.MockPlayer;
import castle.comp3021.assignment.protocol.Color;
import castle.comp3021.assignment.protocol.Configuration;
import castle.comp3021.assignment.protocol.Player;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

/**
 * Put your additional JUnit5 tests for Bonus Task 2 in this class.
 */
public class AdditionalTests {
    private Configuration config;
    private MockPlayer player1;
    private MockPlayer player2;

    @BeforeEach
    public void setUpGame() {
        this.player1 = new MockPlayer(Color.PURPLE);
        this.player2 = new MockPlayer(Color.YELLOW);
        this.config = new Configuration(3, new Player[]{player1, player2});
    }

    @Test
    public void itsTooEarlyToWinOrDie()
    {

    }
}
