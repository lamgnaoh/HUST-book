import random as rd
# 1 cac kieu du lieu co ban
print("############################## part 1")

x = 2
y = 2.5
print(type(x))
print(type(y))
print(rd.randrange(1, 4, 1))
hello = "hello"
world = "world"
print(len(hello))
print(hello)
hw = hello + " " + world
print(hw)
hw12 = '{} and {} and {}'.format(hello, world, 12)
print(hw12)
s = "hello"
print(s.capitalize())  # viet hoa chu cai dau
print(s.upper())  # viet hoa toan bo chu cai
# .replace(old , new , count ) :  thay the old bang new voi so luong thay the bang count
print(s.replace("l", "L", 1))
print("   hello".strip())  # bo khoang trong
# string.split(str , number) : tach string bang chuoi str (mac dinh la dau cach) , number la so phan tu nhieu nhat co the tach(default la -1 : limit). tra ve 1 mang
print("Luong Hoang Lam".split())
# string.join([ ... ]) : join cac phan tu trong array bang chuoi , tra ve 1 chuoi moi
print(".".join(["Luong", "Hoang", "Lam"]))  # Luong.Hoang.Lam

print("############################## part 2")

# 2 Container
# trong python co mot so kieu du lieu co the chua nhieu gia tri (container) nhu list , tuple , set , dictionary

# list
# giong nhu mang(array) nhung khong co dinh kich thuoc va  co the chua nhieu kieu du lieu khac nhau trong 1 list
myList = [1, 2, 3]
print(myList, myList[2])
# chi so am la dem phan tu tu cuoi len ( tuc la phan tu thu 2)
print(myList[-1])
# 1 list co the co nhieu kieu du lieu
myList[2] = "foo"
print(myList)
myList.append("bar")  # them 1 phan tu vao cuoi mang
print(myList)
#  bo di phan tu cuoi cung cua mang va tra ve phan tu do
x = myList.pop()
print(x, myList)
# Slicing : Ho tro truy xuat nhieu phan tu cung mot luc
nums = list(range(5))
print(nums)
print(nums[2:4])  # lay phan tu thu 2-> thu 4
print(nums[2:])  # lay phan tu thu 2 -> het
# loop: duyet va in phan tu trong list
animals = ["cat", "dog", "monkey"]
for animal in animals:
    print('%s' % animal)
for index, animal in enumerate(animals):
    print("%d: %s" % (index, animal))


# dictionaries
# luu thong tin duoi dang key:value(giong object trong javascript)
