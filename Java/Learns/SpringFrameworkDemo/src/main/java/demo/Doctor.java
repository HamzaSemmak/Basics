package demo;

import org.springframework.beans.factory.BeanNameAware;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import javax.annotation.PostConstruct;

@Component
@Scope( scopeName = "singleton")
public class Doctor implements Staff, BeanNameAware {
    private String qualification;
    public String getQualification() {
        return qualification;
    }
    public void setQualification(String qualification) {
        this.qualification = qualification;
    }
    public void assist() {
        System.out.println("doctor is assisting");
    }
    @Override
    public String toString() {
        return "Doctor{" +
                "qualification='" + qualification + '\'' +
                '}';
    }
    @Override
    public void setBeanName(String name) {
        System.out.println("Set Bean name is called.");
    }
    @PostConstruct
    public void postConstruct()
    {
        System.out.println("Post construct method is called");
    }
}
