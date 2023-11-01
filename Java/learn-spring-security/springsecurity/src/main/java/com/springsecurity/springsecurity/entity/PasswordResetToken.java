package com.springsecurity.springsecurity.entity;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.Calendar;
import java.util.Date;

@Entity
@Table(name = "password_rest_token")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class PasswordResetToken {

    private static final int EXPIRATION_TIME = 10;

    @Id
    @SequenceGenerator(
            name = "password_rest_token_sequence",
            sequenceName = "password_rest_token_sequence",
            allocationSize = 1
    )
    @GeneratedValue(
            strategy = GenerationType.SEQUENCE,
            generator = "password_rest_token_sequence"
    )
    private Long id;

    private String token;

    private Date expirationTime;

    @OneToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "user_id", nullable = false, foreignKey = @ForeignKey(name = "FK_USER_PASSWORD_TOKEN"))
    private User user;

    public PasswordResetToken(User user, String token)
    {
        super();
        this.user = user;
        this.token = token;
        this.expirationTime = calculateExpirationTime(EXPIRATION_TIME);
    }

    public PasswordResetToken(String token)
    {
        this.token = token;
        this.expirationTime = calculateExpirationTime(EXPIRATION_TIME);
    }

    private Date calculateExpirationTime(int expirationTime) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(new Date().getTime());
        calendar.add(Calendar.MINUTE, expirationTime);
        return new Date(calendar.getTime().getTime());
    }

}
