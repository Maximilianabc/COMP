package castle.comp3021.assignment.gui.views;

import javafx.scene.Node;
import javafx.scene.control.Button;

/**
 * Helper class for a {@link Button} with "big-button" style applied.
 */
public class BigButton extends Button {

    public BigButton() {
        super();
    }

    public BigButton(String text) {
        super(text);
    }

    public BigButton(String text, Node graphic) {
        super(text, graphic);
    }

    {
        // TODO: Add "big-button" style
    }
}
