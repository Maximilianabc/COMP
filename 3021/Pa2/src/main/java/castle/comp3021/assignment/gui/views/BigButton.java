package castle.comp3021.assignment.gui.views;

import castle.comp3021.assignment.gui.controllers.ResourceLoader;
import javafx.geometry.Pos;
import javafx.scene.Node;
import javafx.scene.control.Button;

import java.io.File;
import java.net.MalformedURLException;

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
