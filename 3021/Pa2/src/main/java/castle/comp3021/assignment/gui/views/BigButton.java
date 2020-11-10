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
        getStyleClass().add("big-button");
    }

    public BigButton(String text, Node graphic) {
        super(text, graphic);
    }

    {
        getStyleClass().add("big-button");
        setMinHeight(40);
        setMinWidth(200);
    }
}
