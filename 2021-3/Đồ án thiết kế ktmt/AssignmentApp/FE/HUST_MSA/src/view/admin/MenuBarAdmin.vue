<template>
    <div class="menu-container">
        <div class="logo-container">
            <a href="https://amisapp.misa.vn/home" class="menu-container-option"></a>
            <a href="" class="logo">
                <img src="../../assets/imgs/logoimg.png" alt="img-icon">
            </a>
        </div>
        <div class="menu-list">
            <div class="tooltip">
                <router-link to="/admin/accountmanage" class="tooltip-content ">
                    
                    <div class="icon-tooltip dashboard-icon">
                        <font-awesome-icon class="icon-20" icon="list-check"/>
                    </div>
                    <div class="title-tooltip">Quản lý tài khoản</div>
                </router-link>
            </div>

        </div>
    </div>
</template>

<script>
export default {

}
</script>

<style>
.menu-container {
display: flex;
flex-direction: column;
background-color: rgb(56, 57, 61);
min-width: 208px;
}
.menu-container .logo-container {
display: flex;
flex-direction: row;
height: 48px;
background-color: rgb(56, 57, 61);
justify-items: center;
align-items: center;

}

.logo-container .menu-container-option {
display: flex;
width: 24px;
height: 24px;
margin-right: 10px;
margin-left: 24px;
}

.logo-container .logo {
    display: flex;
    height: 150px;
    flex-grow: 2;
    margin-left: -33px;
}

.menu-list .tooltip-content {
display: flex;
text-decoration: none;
color: rgb(255, 255, 255);
justify-items: center;
align-items: center;
font-size: 13px;
padding-left: 16px;
height: 42px;
cursor: pointer;
}

.tooltip-content .icon-tooltip {
height: 24px;
width: 24px;
margin-right: 10px;
justify-items: center;
align-items: center;
}

.tooltip-content .title-tooltip {
font-weight: 400;
font-size: 13px;

}

.tooltip .router-link-active {
background-color: #58595d;
border-left: 8px solid #6558ef;
box-sizing: border-box;
font-weight: 600;
}

.router-link-active .icon-tooltip {
position: relative;
font-weight: 600;

left: -8px;
}

.router-link-active .title-tooltip {
position: relative;
font-weight: 600;
/* color: #ffcb3e; */
left: -8px;
}

.router-link-active .deposit-icon {
background: url('../../assets/imgs/Sprites.64af8f61.svg') no-repeat -119px -1585px;
}

.router-link-active .cash-icon {
background: url('../../assets/imgs/Sprites.64af8f61.svg') no-repeat -75px -1585px;
}

.menu-list .tooltip-content:hover {
background-color: #58595d;
}
</style>