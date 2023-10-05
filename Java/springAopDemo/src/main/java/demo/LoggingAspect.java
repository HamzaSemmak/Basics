package demo;

import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.annotation.After;
import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;
import org.springframework.stereotype.Component;

@Aspect
@Component
public class LoggingAspect {

    @Before("execution(* demo.ShoppingCart.checkout(..))")
    public void beforeLogger(JoinPoint jp)
    {
        System.out.println(jp.getSignature());
        String str = jp.getArgs()[0].toString();
        System.out.println(str);
        System.out.println("beforeLogger...");
    }

    @After("execution(* *.*.checkout(..))")
    public void afterLogger()
    {
        System.out.println("afterLogger...");
    }
}
