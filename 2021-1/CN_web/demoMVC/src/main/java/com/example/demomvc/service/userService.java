package com.example.demomvc.service;

import com.example.demomvc.model.User;

public class userService {
    public boolean checkLogin(User user){
        if("admin".equals(user.getUsername()) && "123456".equals(user.getPassword())) {
            return true;
        }else  {
            return false;
        }
    }
}
