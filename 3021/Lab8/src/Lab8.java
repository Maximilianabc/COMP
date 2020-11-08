import java.util.*;

public class Lab8 {
    /**
     * TimeTickService object
     */
    public static final TimeTickService service = new TimeTickService();

    /**
     * The timer used to emit events in TimeTickService
     */
    private static final Timer timer = new Timer();

    /**
     * Emit {@link TimeTickService.TimeTickEvent} every 1 second, with the current {@link Date} object as event source.
     */
    public static void emitEventEverySecond() {
        var task = new TimerTask() {
            @Override
            public void run() {
                var now = new Date();
                service.emitEvent(new TimeTickService.TimeTickEvent(now));
            }
        };
        timer.schedule(task, 1000, 1000);
    }

    /**
     * Stop emitting more events
     */
    public static void cancelEmittingEvent() {
        timer.cancel();
    }

    /**
     * make use of {@link Lab8#emitEventEverySecond()} and {@link Lab8#cancelEmittingEvent()} to start and stop
     * emitting {@link TimeTickService.TimeTickEvent} in {@link TimeTickService}, in order to implement the following
     * functionality:
     * <p>
     * 1. When each event comes, print the time stored in the {@link TimeTickService.TimeTickEvent} object in the
     * console
     * <p>
     * 2. Cancel emitting more events after three events, which means console only prints three events.
     */
    public static void main(String[] args) {
        service.addListener(e -> System.out.println(e));
        emitEventEverySecond();
        try
        {
            Thread.sleep(3001);
            cancelEmittingEvent();
        }
        catch (InterruptedException e)
        {
            e.printStackTrace();
        }
    }
}