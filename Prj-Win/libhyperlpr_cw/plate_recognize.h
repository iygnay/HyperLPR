#pragma once

#ifdef LIBHYPERLPRCW_EXPORTS
#define LIBHYPERLPRCW_API __declspec(dllexport)   
#else  
#define LIBHYPERLPRCW_API __declspec(dllimport)   
#endif

extern "C" {
	LIBHYPERLPRCW_API int __stdcall plate_recognize(
		const char* detector_filename,
		const char* finemapping_prototxt,
		const char* finemapping_caffemodel,
		const char* segmentation_prototxt,
		const char* segmentation_caffemodel,
		const char* charRecognization_proto,
		const char* charRecognization_caffemodel,
		const char* imageFileName,
		char* buffer,
		int bufferSize
	);
}
