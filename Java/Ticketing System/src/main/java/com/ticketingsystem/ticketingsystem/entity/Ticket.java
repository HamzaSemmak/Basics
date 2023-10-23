package com.ticketingsystem.ticketingsystem.entity;

import jakarta.persistence.*;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;
import org.springframework.boot.autoconfigure.web.WebProperties;

import java.util.Date;

@Entity
@Table(name = "tickets")
public class Ticket {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "ticket_id")
    private Long ID;
    
    @Column(name = "title")
    private String Title;
    
    @Column(name = "description")
    private String Description;

    @Column(name = "category")
    private String Category;

    @Column(name = "priority")
    private String Priority;

    @Column(name = "status")
    private String Status;

    @ManyToOne
    @JoinColumn(name = "created_by", referencedColumnName = "user_id")
    private User CreatedBy;

    @ManyToOne
    @JoinColumn(name = "assigned_to", referencedColumnName = "user_id")
    private User AssignedTo;

    @Column(name = "created_at")
    @CreationTimestamp
    @Temporal(TemporalType.TIMESTAMP)
    private Date createdAt;

    @Column(name = "updated_at")
    @UpdateTimestamp
    @Temporal(TemporalType.TIMESTAMP)
    private Date updatedAt;

    public Ticket() {
    }

    public Ticket(String title, String description, String category, String priority, String status, User createdBy, User assignedTo) {
        Title = title;
        Description = description;
        Category = category;
        Priority = priority;
        Status = status;
        CreatedBy = createdBy;
        AssignedTo = assignedTo;
    }

    public Long getID() {
        return ID;
    }

    public void setID(Long ID) {
        this.ID = ID;
    }

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getCategory() {
        return Category;
    }

    public void setCategory(String category) {
        Category = category;
    }

    public String getPriority() {
        return Priority;
    }

    public void setPriority(String priority) {
        Priority = priority;
    }

    public String getStatus() {
        return Status;
    }

    public void setStatus(String status) {
        Status = status;
    }

    public User getCreatedBy() {
        return CreatedBy;
    }

    public void setCreatedBy(User createdBy) {
        CreatedBy = createdBy;
    }

    public User getAssignedTo() {
        return AssignedTo;
    }

    public void setAssignedTo(User assignedTo) {
        AssignedTo = assignedTo;
    }

    public Date getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Date createdAt) {
        this.createdAt = createdAt;
    }

    public Date getUpdatedAt() {
        return updatedAt;
    }

    public void setUpdatedAt(Date updatedAt) {
        this.updatedAt = updatedAt;
    }

    @Override
    public String toString() {
        return "Ticket{" +
                "ID=" + ID +
                ", Title='" + Title + '\'' +
                ", Description='" + Description + '\'' +
                ", Category='" + Category + '\'' +
                ", Priority='" + Priority + '\'' +
                ", Status='" + Status + '\'' +
                ", CreatedBy=" + CreatedBy +
                ", AssignedTo=" + AssignedTo +
                ", createdAt=" + createdAt +
                ", updatedAt=" + updatedAt +
                '}';
    }
}
