import org.junit.jupiter.api.Test;

import java.util.*;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;
import java.util.stream.Collectors;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class TimeTickTests {
    @Test
    public void testOnlyThreeTickEvent() throws ExecutionException, InterruptedException {
        List<EventObject> eventList = new ArrayList<>();
        CompletableFuture<String> future = new CompletableFuture<>();
        new Timer().schedule(new TimerTask() {
            @Override
            public void run() {
                // check if there is only three events
                assertEquals(3, eventList.size());
                var times = eventList.stream()
                        .map(eventObject -> (Date) eventObject.getSource())
                        .collect(Collectors.toList());
                // test the interval is roughly 1 second
                assertEquals(1, Math.round(((double) times.get(1).getTime() - times.get(0).getTime()) / 1000));
                assertEquals(1, Math.round(((double) times.get(2).getTime() - times.get(1).getTime()) / 1000));
                future.complete("test pass");
            }
        }, 5000);
        Lab8.service.addListener(eventList::add);
        Lab8.main(new String[]{});
        assertEquals("test pass", future.get());
    }
}
