package LearnJDBC.Service;

import java.util.Map;

public class MigrationService {
    public String MapPipe(Map<String, String> map)
    {
        StringBuilder result = new StringBuilder();
        for (Map.Entry<String, String> entry : map.entrySet()) {
            result.append(entry.getKey()).append(" ").append(entry.getValue()).append(", ");
        }
        if (result.length() > 2) {
            result.setLength(result.length() - 2);
        }
        return result.toString();
    }
}
