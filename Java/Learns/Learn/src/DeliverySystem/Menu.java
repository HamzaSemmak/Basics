package DeliverySystem;

import Logger.log4j;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class Menu {
    private List<MenuItem> listOfMenuItems;

    public List<MenuItem> getListOfMenuItems() {
        return listOfMenuItems;
    }

    public void setListOfMenuItems(List<MenuItem> listOfMenuItems) {
        this.listOfMenuItems = listOfMenuItems;
    }

    public Menu() {
        listOfMenuItems = new ArrayList<>();
    }

    public void addMenuItem(MenuItem item)
    {
        listOfMenuItems.add(item);
        log4j.info("(1) Row affected Successfully." + item.toString());
    }

    public void removeMenuItem(MenuItem item)
    {
        listOfMenuItems.remove(item);
        log4j.info("(1) Row deleted Successfully." + item.toString());
    }

    public void listMenu()
    {
        log4j.info("This the list of Menu items : ");
        for(MenuItem item: listOfMenuItems)
        {
            log4j.info(item.toString());
            log4j.info("\n");
        }
    }

    @Override
    public String toString() {
        return "Menu{" +
                "listOfMenuItems=" + listOfMenuItems +
                '}';
    }

    public void searchMenuItem(String keyword)
    {
        log4j.info("Search...");
        List<MenuItem> items = listOfMenuItems.stream()
                .filter(item ->
                        item.getCategory().toLowerCase().contains(keyword.toLowerCase())
                        || item.getName().toLowerCase().contains(keyword.toLowerCase())
                ).collect(Collectors.toList());

        if (!items.isEmpty()) {
            for(MenuItem item : listOfMenuItems)
            {
                log4j.info(item.toString());
            }
        } else {
            log4j.info("0 row was found.");
        }
    }
}
