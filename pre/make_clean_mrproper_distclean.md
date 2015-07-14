After downloading Linux source code, run

    make help

    Cleaning targets:
                clean           - Remove most generated files but keep the config and
                                enough build support to build external modules
                mrproper        - Remove all generated files + config + various backup files
                distclean       - mrproper + remove editor backup and patch files

Open Makefile

    clean: archclean $(clean-dirs)
                $(call cmd,rmdirs)
                $(call cmd,rmfiles)
                @find . $(RCS_FIND_IGNORE) \
                        \( -name '*.[oas]' -o -name '*.ko' -o -name '.*.cmd' \
                        -o -name '.*.d' -o -name '.*.tmp' -o -name '*.mod.c' \
                        -o -name '*.symtypes' -o -name 'modules.order' \
                        -o -name 'Module.markers' \) \
                        -type f -print | xargs rm -f
 
    mrproper: clean archmrproper $(mrproper-dirs)
                $(call cmd,rmdirs)
                $(call cmd,rmfiles)
 
    distclean: mrproper
             @find $(srctree) $(RCS_FIND_IGNORE) \
                        \( -name '*.orig' -o -name '*.rej' -o -name '*~' \
                        -o -name '*.bak' -o -name '#*#' -o -name '.*.orig' \
                        -o -name '.*.rej' -o -size 0 \
                        -o -name '*%' -o -name '.*.cmd' -o -name 'core' \) \
                        -type f -print | xargs rm -f

So, when executing make mproper, first it executes make clean, then it executes make mrproper, and then it executes make distclean.
