package castle.comp3021.assignment.gui.views;

import javafx.scene.Node;
import javafx.scene.layout.VBox;

/**
 * Helper class for a {@link VBox} with "side-menu" style applied.
 */
public class SideMenuVBox extends VBox {
    public SideMenuVBox() {
        super(20);
    }

    public SideMenuVBox(double spacing) {
        super(spacing);
    }

    public SideMenuVBox(Node... children) {
        super(children);
    }

    public SideMenuVBox(double spacing, Node... children) {
        super(spacing, children);
    }

    {
        getStyleClass().add("side-menu");
    }
}
