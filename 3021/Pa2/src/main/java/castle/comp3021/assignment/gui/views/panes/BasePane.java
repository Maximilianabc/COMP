package castle.comp3021.assignment.gui.views.panes;

import castle.comp3021.assignment.protocol.Configuration;
import javafx.scene.layout.BorderPane;

/**
 * Abstraction for a {@link BorderPane} which can act as the root of a {@link javafx.scene.Scene}.
 */
public abstract class BasePane extends BorderPane {
    /**
     * Shared configuration by all panes
     */
    protected static Configuration globalConfiguration = new Configuration();
    /**
     * Connects all components into the {@link BorderPane}.
     */
    abstract void connectComponents();

    /**
     * Styles all components as required.
     */
    abstract void styleComponents();

    /**
     * Set callbacks for all interactive components.
     */
    abstract void setCallbacks();
}
