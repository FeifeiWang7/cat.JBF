##### Concepts
Three main states of git
 * committed - the data is safely stored in your local database
 * modified - have changed the file but have not committed it to the database
 * staged - have marked a modified file in its current version to go into the next commit snapshot

working dir     staging area        .git dir (Repository)
    <---------------------------------check out proj
    ---stage fixes---->
                    ---commit--------------->

The Git dir is where Git stores the metadata and object database for the project. It is what is copied when you clone a repository from another computer.

The working dir is a single checkout of one version of the project. These files are pulled out of the compressed database in the Git directory and placed on disk for you to use or modify.

The staging area is a file, generally contained in your Git directory, that stores information about what will go into your next commit. It’s sometimes referred to as the “index”, but it’s also common to refer to it as the staging area.

The basic Git workflow goes something like this:
 * You modify files in your working directory.
 * You stage the files, adding snapshots of them to your staging area.
 * You do a commit, which takes the files as they are in the staging area and stores that snapshot permanently to your Git directory.

If a particular version of a file is in the Git directory, it’s considered committed. If it has been modified and was added to the staging area, it is staged. And if it was changed since it was checked out but has not been staged, it is modified.

##### Setup
Bake identity into the commits:

git config --global user.name "FeifeiWang7"
git config --global user.email fwang12@ncsu.edu

Default text editor (type in commit message):
git config --global core.editor vim

check settings:
git config --list

https://git-scm.com/book/en/v2/Git-Basics-Getting-a-Git-Repository
