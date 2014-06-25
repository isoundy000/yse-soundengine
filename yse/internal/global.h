/*
  ==============================================================================

    global.h
    Created: 27 Jan 2014 10:18:27pm
    Author:  yvan

  ==============================================================================
*/

#ifndef GLOBAL_H_INCLUDED
#define GLOBAL_H_INCLUDED

#include "JuceHeader.h"
#include "../headers/types.hpp"
#include "../classes.hpp"

namespace YSE {  
    
  namespace INTERNAL {
    
    class global {
    public:
      bool isActive() { return active; }

      logImplementation & getLog();
      listenerImplementation & getListener();

      void addSlowJob(ThreadPoolJob * job);
      void addFastJob(ThreadPoolJob * job);
      void waitForSlowJob(ThreadPoolJob * job);
      void waitForFastJob(ThreadPoolJob * job);
      bool containsSlowJob(ThreadPoolJob * job);
      
      void flagForUpdate() { update++;  }
      bool needsUpdate() { return update > 0;  }
      void updateDone() { update--; }

      global();

    private:
      void init();
      void close();

      logImplementation * log;
      listenerImplementation * li;


      ThreadPool slowThreads;
      ThreadPool fastThreads;

      aInt update;
      aBool active; // set true after System().init(), false at System().close()

      friend class YSE::system; // system needs access to the init and close method
    };

    global & Global();
  }
}



#endif  // GLOBAL_H_INCLUDED