<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
    <title>JSP - Hello World</title>
</head>
<body>

        <div>
            <h1>Đăng nhập</h1>
        </div>
        <form action="<%=request.getContextPath()%>/welcome" method="post">
            <div>
                <label>Tên đăng nhập</label>
                <input type="text" name="username">
            </div>
            <div>
                <label>Mật khẩu</label>
                <input type="password" name="password">
            </div>
            <input type="submit" value="Login" name="submit">
        </form>
</body>
</html>