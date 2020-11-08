import java.util.*;

/**
 * TimeTickService support event emitting and listening.
 * <p>
 * The following service is provided:
 * <p>
 * 1. Listeners can be registered to listen to each event emitted by the service
 * <p>
 * 2. Registered can be unregistered
 * <p>
 * 3. Offers an public method {@link TimeTickService#emitEvent(TimeTickEvent)} which can be called to emit events to
 * all registered listeners.
 * <p>
 * 4. {@link TimeTickEvent} should be emitted every 1 seconds. Students don't need to implement this, we have already
 * implemented for you in {@link Lab8#emitEventEverySecond()}.
 */
public class TimeTickService {
    /**
     * The event that is emitted by {@link TimeTickService}
     * <p>
     * This class should extend {@link EventObject}, which is the utility class for an Event
     */
    public static class TimeTickEvent extends EventObject{

        /**
         * Constructs an Event.
         *
         * @param now the current time when the event is emitted, this parameter will be passed to constructor of
         *            {@link EventObject} as event source.
         * @throws IllegalArgumentException if now is null
         */
        public TimeTickEvent(Date now) {
            super(now);
        }
    }

    /**
     * The listener to listen to {@link TimeTickEvent} that is emitted by {@link TimeTickService}
     * <p>
     * This interface should extend {@link EventListener}, which is tag for event listeners.
     */
    @FunctionalInterface
    public static interface TimeTickListener extends EventListener {
        /**
         * The method as a handler of the {@link TimeTickEvent}.
         * This methods has only one parameter: an {@link TimeTickEvent} object and does not have return value.
         *
         * @param event the event object
         */
        // declare one handler method, for example "onTimeTickEvent(...)"
        public void onTimeTickEvent(TimeTickEvent event);
    }

    /**
     * A list of listeners registered to listen to events
     */
    // define a field which holds all registered listeners.
    private ArrayList<TimeTickListener> listeners;

    /**
     * Constructor
     */
    public TimeTickService() {
        // do some necessary initialization
        listeners = new ArrayList<>();
    }

    /**
     * Register a new listener
     *
     * @param listener the event listener
     */
    public void addListener(TimeTickListener listener) {
        listeners.add(listener);
    }

    /**
     * Unregister a listener
     *
     * @param listener event listener
     */
    public void removeListener(TimeTickListener listener) {
        listeners.remove(listener);
    }

    /**
     * Emit the given {@link TimeTickEvent} to every registered listeners
     *
     * @param event the event to emit
     */
    public void emitEvent(TimeTickEvent event) {
        for (TimeTickListener listener : listeners)
        {
            listener.onTimeTickEvent(event);
        }
    }
}


