package castle.comp3021.assignment.gui.views;

import castle.comp3021.assignment.gui.ViewConfig;
import castle.comp3021.assignment.gui.controllers.ResourceLoader;
import javafx.scene.Node;
import javafx.scene.layout.VBox;

import java.io.File;
import java.net.MalformedURLException;

/**
 * Helper class for a {@link VBox} with "big-vbox" style applied.
 */
public class BigVBox extends VBox {

    /**
     * Creates an instance with spacing of 20.
     */
    public BigVBox() {
        super(10);
    }

    public BigVBox(double spacing) {
        super(spacing);
    }

    public BigVBox(Node... children) {
        super(children);
    }

    public BigVBox(double spacing, Node... children) {
        super(spacing, children);
    }

    {
        getStyleClass().add("big-vbox");
    }
}
