package castle.comp3021.assignment.gui.controllers;

import javafx.scene.media.MediaPlayer;

import java.util.HashSet;
import java.util.Set;

/**
 * Handles audio related events.
 */
public class AudioManager {

    private static final AudioManager INSTANCE = new AudioManager();
    /**
     * Set of all currently playing sounds.
     *
     * <p>
     * Use this to keep a reference to the sound until it finishes playing.
     * </p>
     */
    private final Set<MediaPlayer> soundPool = new HashSet<>();
    private boolean enabled = true;

    /**
     * Enumeration of known sound resources.
     */
    public enum SoundRes {
        WIN, PLACE, CLICK,LOSE;
        @Override
        public String toString() {
            return super.toString().toLowerCase();
        }
    }

    private AudioManager() {
    }

    public static AudioManager getInstance() {
        return INSTANCE;
    }

    public boolean isEnabled() {
        return enabled;
    }

    public void setEnabled(boolean enabled) {
        this.enabled = enabled;
    }

    /**
     * Plays the sound. If disabled, simply return.
     *
     * <p>
     * Hint:
     * <ul>
     * <li>Use {@link MediaPlayer#play()} and {@link MediaPlayer#dispose()}.</li>
     * <li>When creating a new MediaPlayer object, add it into {@link AudioManager#soundPool} before playing it.</li>
     * <li>Set a callback for when the sound has completed playing. This is to remove it from the soundpool, and
     * dispose the player using a daemon thread.</li>
     * </ul>
     *
     * @param name the name of the sound file to be played, excluding .mp3
     */
    private void playFile(final String name) {
        //TODO

    }

    /**
     * Plays a sound.
     *
     * @param name Enumeration of the sound, given by {@link SoundRes}.
     */
    public void playSound(final SoundRes name) {
        playFile(name.toString());
    }
}