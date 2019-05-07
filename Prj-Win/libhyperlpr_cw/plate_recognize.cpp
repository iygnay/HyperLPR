#include "stdafx.h"
#include "plate_recognize.h"
#include "pipeline.h"
#include <string>
#include <sstream>

int plate_recognize(
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
)
{
	pr::PipelinePR prc(detector_filename,
		finemapping_prototxt, finemapping_caffemodel,
		segmentation_prototxt, segmentation_caffemodel,
		charRecognization_proto, charRecognization_caffemodel
	);

	cv::Mat image = cv::imread(imageFileName);
	auto res = prc.RunPiplineAsImage(image, pr::SEGMENTATION_BASED_METHOD);
	std::stringstream ss;

	for (auto st : res) {
		ss << st.getPlateName() << " " << st.getPlateType() << " " << st.confidence << "#";
	}
	
	// 输出结果
	size_t i;
	auto data = ss.str();

	for (i = 0; i < data.size() && i < bufferSize; i++)
		buffer[i] = data[i];

	// 返回
	return static_cast<int>(data.size());
	//return static_cast<int>(res.size());
}
