package castle.comp3021.assignment.gui.controllers;

import castle.comp3021.assignment.protocol.exception.ResourceNotFoundException;
import javafx.scene.image.Image;
import org.jetbrains.annotations.NotNull;

import java.io.File;
import java.nio.file.Path;
import java.nio.file.Paths;

/**
 * Helper class for loading resources from the filesystem.
 */
public class ResourceLoader {
    /**
     * Path to the resources directory.
     */
    @NotNull
    private static final Path RES_PATH;

    static {
        // Initialize RES_PATH
        // replace null to the actual path
        RES_PATH = Paths.get("src/main/resources/");
    }

    /**
     * Retrieves a resource file from the resource directory.
     *
     * @param relativePath Path to the resource file, relative to the root of the resource directory.
     * @return Absolute path to the resource file.
     * @throws ResourceNotFoundException If the file cannot be found under the resource directory.
     */
    @NotNull
    public static String getResource(@NotNull final String relativePath) {
        String fullPath = Paths.get(RES_PATH.toString(), relativePath).toString();
        if (new File(fullPath).exists()) {
            return fullPath;
        } else {
            throw new ResourceNotFoundException("No resources is located at specified path.");
        }
    }

    /**
     * Return an image {@link Image} object
     * @param typeChar a character represents the type of image needed.
     *                 - 'K': white knight (whiteK.png)
     *                 - 'A': white archer (whiteA.png)
     *                 - 'k': black knight (blackK.png)
     *                 - 'a': black archer (blackA.png)
     *                 - 'c': central x (center.png)
     *                 - 'l': light board (lightBoard.png)
     *                 - 'd': dark board (darkBoard.png)
     * @return an image
     */
    @NotNull
    public static Image getImage(char typeChar) {
        return switch (typeChar) {
            case 'K' -> new Image(getResource("images/whiteK.png"));
            case 'A' -> new Image(getResource("images/whiteA.png"));
            case 'k' -> new Image(getResource("images/blackK.png"));
            case 'a' -> new Image(getResource("images/blackA.png"));
            case 'c' -> new Image(getResource("images/center.png"));
            case 'l' -> new Image(getResource("images/lightboard.png"));
            case 'd' -> new Image(getResource("images/darkBoard.png"));
            default -> throw new IllegalStateException("Unexpected value: " + typeChar);
        };
    }


}