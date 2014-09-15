#ifndef DISPLAYCLASS_H
#define DISPLAYCLASS_H
#include <iostream>
#include <iomanip>
#include <string>
using namespace std;

class DisplayClass
{
private:
    static const string commands[11], command_help[11] ;
    void insert();
    void _delete();
    
    void truncate();
    void set();
    void quit();
    void help();
    
    
    void run_NotOperating();
	void run_Operating();
    void view();
    
    void prompt();
    void _default();
public:

    void i();
    void d();
   
    void t();
    void e();
    void h();
    void r_Operating();
	void r_NotOperating();
    void q();
   
    
    void v();
    
    void p();
    void _d();
   
};

#endif // DISPLAYCLASS_H
