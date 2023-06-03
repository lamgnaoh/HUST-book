package com.example.demomvc.controller;

import com.example.demomvc.model.User;
import com.example.demomvc.service.userService;

import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import java.io.IOException;


/*
* Servlet xử lý đăng nhập và điều hướng
*
* */
@WebServlet(name = "LoginController", value = "/LoginController")
public class LoginController extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        RequestDispatcher rd = request.getRequestDispatcher("/index.jsp");
        rd.forward(request,response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String username = request.getParameter("username");
        String password = request.getParameter("password");
        userService service = new userService();
        User user   = new User(username,password);
        if(service.checkLogin(user)){
            response.sendRedirect("welcome");
        }else{
            response.sendRedirect("login?err=1");
        }
    }
}
