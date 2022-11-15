# What
- Template use for setup setting you need for create unity package
- Replace your setting in file package.json


# How To Install Package

Add the lines below to `Packages/manifest.json`

for version 1.0.0
```csharp
    "com.pancake.heart": "https://github.com/pancake-llc/heart.git#1.0.1",
    "com.gamee.rope2d": "https://github.com/gamee-studio/Rope2D.git?path=Assets/_Root",
```
# How to use
![Screenshot 2022-11-15 113839](https://user-images.githubusercontent.com/92133266/201827671-eb60ca06-38ee-40e0-ad5b-de6bc8d11773.png)
Click to Edit rope -> drag 2 note End1 or End2 to change the length of ropeline.
Some properties like Scale, Spacing, Smoothness Nodes count... be Used to change properties node_line of ropeline.
