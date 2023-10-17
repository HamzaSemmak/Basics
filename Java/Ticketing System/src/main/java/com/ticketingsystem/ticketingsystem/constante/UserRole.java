package com.ticketingsystem.ticketingsystem.constante;

public enum UserRole {
    CUSTOMER("Customer"),
    SUPPORT_AGENT("Support Agent");

    private final String displayName;

    UserRole(String displayName) {
        this.displayName = displayName;
    }

    public String getDisplayName() {
        return displayName;
    }
}
