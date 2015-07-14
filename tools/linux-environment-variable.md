### Linux Environment Variable

There are two types:

* permanent - modify config file
* temporary - use export, ineffective when close shell

#### Permanent

Effective to all users

    /etc/profile
    source /etc/profile

Effective to current user

    .bash_profile
    source /home/guok/.bash_profile
    
#### Temporary

For current shell (bash)

    export  xxx=yyy

#### Other

Show environment variable xxx

    echo $xxx

Show all environment variables

    env
or

    env | grep xxx
    
Show all local shell variables

    set
    
Clean environment variable xxx

    unset $XX
    
Make environment variable xxx readonly

    export xxx=yyy
    readonly xxx
    unset xxx        #该变量不可被删除
    xxx=yyy          #该变量不可被修改
